namespace Vezeeta.Features.Patients.Models;


public record PatientDetailsResponse(
    string? Image,
    string FullName,
    string Email,
    string Phone,
    string Gender,
    DateOnly DateOfBirth,
    List<PatientBookingResponse> Requests
);

public record PatientBookingResponse(
    string? Image,
    string DoctorName,
    string Specialization,
    string Day,
    string Time,
    decimal Price,
    string? DiscountCode,
    decimal FinalPrice,
    string Status
);