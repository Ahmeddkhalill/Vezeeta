namespace Vezeeta.Features.Doctors.Commands.DeleteDoctorSchedule;

public class DeleteDoctorScheduleHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
    : IRequestHandler<DeleteDocterScheduleRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = accessor;

    public async Task<Result> Handle(DeleteDocterScheduleRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Result.Failure(DoctorErrors.Unauthorized);

        var doctor = await _context.Doctors
            .Include(d => d.DoctorSchedules)
            .ThenInclude(s => s.TimeSlots)
            .FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);

        if (doctor is null)
            return Result.Failure(DoctorErrors.DoctorNotFound);

        var schedule = doctor.DoctorSchedules.FirstOrDefault(s => s.Id == request.ScheduleId);
        if (schedule is null)
            return Result.Failure(DoctorErrors.ScheduleNotFound);

        if (schedule.TimeSlots.Any(ts => ts.IsBooked))
            return Result.Failure(DoctorErrors.ScheduleDeleteNotAllowed);

        _context.DoctorTimeSlots.RemoveRange(schedule.TimeSlots);
        _context.DoctorSchedules.Remove(schedule);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
