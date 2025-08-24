using DayOfWeek = Vezeeta.Entities.DayOfWeek;

namespace Vezeeta.Features.Doctors.Commands.AddDoctorSchedule;

public record AddDoctorScheduleRequest(
    List<AddDoctorDayDto> Days
) : IRequest<Result>;

public record AddDoctorDayDto(
    DateOnly Date,
    DayOfWeek DayOfWeek,
    List<AddDoctorTimeSlotDto> TimeSlots
);

public record AddDoctorTimeSlotDto(
    TimeSpan StartTime,
    TimeSpan EndTime,
    decimal Price
);