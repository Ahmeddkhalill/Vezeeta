using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vezeeta.Entities;
using Vezeeta.Features.Doctors.Models;

namespace Vezeeta.Features.Doctors.Queries.GetAllBookings;

public class GetDoctorBookingsHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
    : IRequestHandler<GetDoctorBookingsRequest, IEnumerable<DoctorBookingResponse>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _accessor = accessor;

    public async Task<IEnumerable<DoctorBookingResponse>> Handle(GetDoctorBookingsRequest request, CancellationToken cancellationToken)
    {
        var userId = _accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);

        if (doctor is null)
            return new List<DoctorBookingResponse>();

        var bookings = await _context.Bookings
            .Include(b => b.DoctorTimeSlot)
            .Include(b => b.DoctorSchedule)
            .Include(b => b.Patient)
            .Where(b => b.DoctorId == doctor.Id)
            .Select(b => new DoctorBookingResponse(
                b.Id,
                b.Patient!.FirstName + " " + b.Patient.LastName,
                b.Patient.Email!,
                b.DoctorSchedule!.Date,
                b.DoctorTimeSlot!.StartTime.ToString(@"hh\:mm"),
                b.DoctorTimeSlot.EndTime.ToString(@"hh\:mm"),
                b.FinalPrice,
                b.IsConfirmed,
                b.CouponCode
            ))
            .ToListAsync(cancellationToken);

        return bookings;
    }
}
