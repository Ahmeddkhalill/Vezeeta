namespace Vezeeta.Features.Admins.Doctors.Commands.AddDoctor;

public record AddDoctorRequest
(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    int SpecializationId, 
    IFormFile? Image
) : IRequest<Result>;
