namespace Vezeeta.Features.Patients.Models;

public record PatientResponse(
    string? Image,
    string FullName,
    string Email,
    string Phone,
    Gender Gender,
    DateOnly DateOfBirth
);