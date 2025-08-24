namespace Vezeeta.Features.Admins.Doctors.Models;

public record DoctorResponse(
    int id,
    string Image,
    string FullName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string Gender
);
