public record UpdateDoctorRequest(
    int DoctorId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    int SpecializationId,
    DateOnly DateOfBirth,
    IFormFile? Image
) : IRequest<Result>;