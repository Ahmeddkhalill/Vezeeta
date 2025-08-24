namespace Vezeeta.Features.Admins.Doctors.Models;

public record DoctorDetailsResponse(
    int Id,
    string Image,
    string FullName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string Gender,
    DateOnly DateOfBirth
);