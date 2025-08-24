namespace Vezeeta.Features.Patients.Models;

public record BookingResponse(
    int Id,
    string DoctorName,
    string DoctorEmail,
    DateOnly Date,
    string StartTime,
    string EndTime,
    decimal FinalPrice,
    bool IsConfirmed,
    string? CouponCode
);