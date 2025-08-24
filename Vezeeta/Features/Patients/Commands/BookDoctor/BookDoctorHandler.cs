using Vezeeta.Features.Patients.Models;

namespace Vezeeta.Features.Patients.Commands.BookDoctor;

public class BookDoctorHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
    : IRequestHandler<BookDoctorRequest, Result<BookDoctorResponse>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = accessor;

    public async Task<Result<BookDoctorResponse>> Handle(BookDoctorRequest request, CancellationToken cancellationToken)
    {
        var patientId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (patientId is null)
            return Result.Failure<BookDoctorResponse>(BookingErrors.Unauthorized);

        var schedule = await _context.DoctorSchedules
            .Include(s => s.Doctor)!.ThenInclude(d => d!.User)
            .Include(s => s.TimeSlots)
            .FirstOrDefaultAsync(s => s.Id == request.ScheduleId, cancellationToken);

        if (schedule is null)
            return Result.Failure<BookDoctorResponse>(BookingErrors.InvalidSchedule);

        var timeSlot = schedule.TimeSlots.FirstOrDefault(ts => ts.Id == request.TimeSlotId);
        if (timeSlot is null)
            return Result.Failure<BookDoctorResponse>(BookingErrors.InvalidTimeSlot);

        if (timeSlot.IsBooked)
            return Result.Failure<BookDoctorResponse>(BookingErrors.TimeSlotAlreadyBooked);

        decimal originalPrice = timeSlot.Price;
        decimal finalPrice = originalPrice;
        string? couponApplied = null;

        if (!string.IsNullOrWhiteSpace(request.CouponCode))
        {
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(c =>
                    c.Code == request.CouponCode &&
                    c.IsActive &&
                    c.ExpiryDate > DateTime.UtcNow,
                    cancellationToken);

            if (coupon is null)
                return Result.Failure<BookDoctorResponse>(CouponErrors.NotFound);

            int discountPercent = (int)coupon.Level * 10; 
            finalPrice -= originalPrice * discountPercent / 100;
            couponApplied = coupon.Code;
        }

        timeSlot.IsBooked = true;

        var booking = new Booking
        {
            DoctorId = schedule.DoctorId,
            PatientId = patientId,
            DoctorScheduleId = request.ScheduleId,
            DoctorTimeSlotId = request.TimeSlotId,
            CouponCode = couponApplied,
            FinalPrice = finalPrice,
            IsConfirmed = false 
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new BookDoctorResponse(
            DoctorName: $"{schedule.Doctor!.User!.FirstName} {schedule.Doctor.User.LastName}",
            Date: schedule.Date,
            StartTime: timeSlot.StartTime.ToString(@"hh\:mm"),
            EndTime: timeSlot.EndTime.ToString(@"hh\:mm"),
            OriginalPrice: originalPrice,
            FinalPrice: finalPrice,
            CouponApplied: couponApplied,
            IsConfirmed: booking.IsConfirmed
        );

        return Result.Success(response);
    }
}
