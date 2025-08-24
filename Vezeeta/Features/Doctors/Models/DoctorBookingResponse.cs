namespace Vezeeta.Features.Doctors.Models;

public record DoctorBookingResponse(
    int BookingId,
    string PatientName,
    string PatientEmail,
    DateOnly Date,
    string StartTime,
    string EndTime,
    decimal FinalPrice,
    bool IsConfirmed,
    string? CouponApplied
);