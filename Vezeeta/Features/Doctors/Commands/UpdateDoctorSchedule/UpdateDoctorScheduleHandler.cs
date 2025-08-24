namespace Vezeeta.Features.Doctors.Commands.UpdateDoctorSchedule;

public class UpdateDoctorScheduleHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
    : IRequestHandler<UpdateDoctorScheduleRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = accessor;

    public async Task<Result> Handle(UpdateDoctorScheduleRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var schedule = await _context.DoctorSchedules
            .Include(s => s.TimeSlots)
            .FirstOrDefaultAsync(s => s.Id == request.ScheduleId && s.Doctor!.UserId == userId, cancellationToken);

        if (schedule is null)
            return Result.Failure(DoctorErrors.ScheduleNotFound);

        if (schedule.TimeSlots.Any(ts => ts.IsBooked))
            return Result.Failure(DoctorErrors.ScheduleUpdateNotAllowed);

        _context.DoctorTimeSlots.RemoveRange(schedule.TimeSlots);

        schedule.TimeSlots = request.TimeSlots
            .Select(ts => new DoctorTimeSlot
            {
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                Price = ts.Price
            })
            .ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
