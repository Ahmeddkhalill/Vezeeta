namespace Vezeeta.Features.Doctors.Models;

public record GetAllDoctorsResponse(
    string FullName,
    string Email,
    string Phone,
    string? Image,
    string Specialization,
    decimal Price,
    string Gender,
    List<DoctorScheduleDto> Schedules
);

public record DoctorScheduleDto(
    int Id,
    DateOnly Date,
    string DayOfWeek,
    List<DoctorTimeSlotDto> TimeSlots
);

public record DoctorTimeSlotDto(
    int Id,
    string StartTime,
    string EndTime,
    decimal Price,
    bool IsBooked
);