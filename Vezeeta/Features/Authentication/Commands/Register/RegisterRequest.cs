namespace Vezeeta.Features.Authentication.Commands.Register;

public record RegisterRequest 
    (
        string Email,
        string FirstName,
        string LastName,
        string Password,
        Gender Gender,
        DateOnly DateOfBirth,
        string PhoneNumber,
        IFormFile? Image
    ) : IRequest<AuthResponse?>;
