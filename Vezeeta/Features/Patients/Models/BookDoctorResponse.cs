namespace Vezeeta.Features.Patients.Models;

public record BookDoctorResponse
(
    string DoctorName,
    DateOnly Date,
    string StartTime,
    string EndTime,
    decimal OriginalPrice,
    decimal FinalPrice,
    string? CouponApplied,
    bool IsConfirmed
);