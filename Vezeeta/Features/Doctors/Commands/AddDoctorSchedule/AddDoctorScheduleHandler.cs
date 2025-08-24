using System.Security.Claims;

namespace Vezeeta.Features.Doctors.Commands.AddDoctorSchedule;

public class AddDoctorScheduleHandler(
    ApplicationDbContext context,
    IHttpContextAccessor accessor
) : IRequestHandler<AddDoctorScheduleRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _accessor = accessor;

    public async Task<Result> Handle(AddDoctorScheduleRequest request, CancellationToken cancellationToken)
    {
        var userId = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Result.Failure(DoctorErrors.Unauthorized);

        var doctor = await _context.Doctors
            .Include(d => d.DoctorSchedules)
            .ThenInclude(s => s.TimeSlots)
            .FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);

        //if (doctor is null)
        //    return Result.Failure(DoctorErrors.DoctorNotFound);

        foreach (var day in request.Days)
        {
            if (doctor.DoctorSchedules.Any(s => s.Date == day.Date))
                return Result.Failure(DoctorErrors.ScheduleAlreadyExists);

            var newSchedule = new DoctorSchedule
            {
                DoctorId = doctor.Id,
                Date = day.Date,
                DayOfWeek = day.DayOfWeek,
                TimeSlots = day.TimeSlots.Select(ts => new DoctorTimeSlot
                {
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime,
                    Price = ts.Price
                }).ToList()
            };

            doctor.DoctorSchedules.Add(newSchedule);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
