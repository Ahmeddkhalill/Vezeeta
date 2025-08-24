namespace Vezeeta.Features.Patients.Models;

public record GetAllDoctorsResponse(
    string FullName,
    string Email,
    string Phone,
    string? Image,
    string Specialization,
    string Gender,
    IEnumerable<DoctorScheduleDto> Schedules
);

public record DoctorScheduleDto(
    int Id,
    DateOnly Date,
    string DayOfWeek,
    IEnumerable<DoctorTimeSlotDto> TimeSlots
);

public record DoctorTimeSlotDto(
    int Id,
    string StartTime,
    string EndTime,
    decimal Price
);