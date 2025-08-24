//namespace Vezeeta.Features.Patients.Commands.CancelBook;

//public class CancelBookingHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
//    : IRequestHandler<CancelBookingRequest, Result>
//{
//    private readonly ApplicationDbContext _context = context;
//    private readonly IHttpContextAccessor _accessor = accessor;

//    public async Task<Result> Handle(CancelBookingRequest request, CancellationToken cancellationToken)
//    {
//        var patientId = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
//        if (patientId is null)
//            return Result.Failure(BookingErrors.Unauthorized);

//        var booking = await _context.Bookings
//            .Include(b => b.TimeSlot)
//            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

//        if (booking is null)
//            return Result.Failure(BookingErrors.NotFound);

//        if (booking.PatientId != patientId)
//            return Result.Failure(BookingErrors.NotFoundPatient);

//        if (booking.IsConfirmed)
//            return Result.Failure(BookingErrors.AlreadyConfirmed); // لا ينفع المريض يلغي بعد ما الدكتور أكد

//        // Free the time slot
//        booking.TimeSlot.IsBooked = false;

//        _context.Bookings.Remove(booking);
//        await _context.SaveChangesAsync(cancellationToken);

//        return Result.Success();
//    }
//}