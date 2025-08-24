namespace Vezeeta.Features.Doctors.Commands.UpdateDoctorSchedule;

public record UpdateDoctorScheduleRequest(
    int ScheduleId, 
    List<UpdateDoctorTimeSlotDto> TimeSlots 
) : IRequest<Result>;

public record UpdateDoctorTimeSlotDto(
    TimeSpan StartTime,
    TimeSpan EndTime,
    decimal Price
);
