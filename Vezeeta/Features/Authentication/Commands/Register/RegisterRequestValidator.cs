using Vezeeta.Abstractions.Consts;
namespace Vezeeta.Features.Authentication.Commands.Register;
public class RegisterRequestValidator : AbstractValidator<RegisterRequest> 
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(50); 
        RuleFor(x => x.DateOfBirth).NotEmpty().Must(BeValidBirthDate).WithMessage("Birth date must be a valid past date."); 
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(RegexPatterns.PhoneNumber).WithMessage("Phone number must be 10 or 11 digits.");
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password).WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
    }
    private bool BeValidBirthDate(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow); return dateOfBirth != default && dateOfBirth <= today; 
    }
}