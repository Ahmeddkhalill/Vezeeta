using FluentValidation;

namespace Vezeeta.Features.Authentication.Queries.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required and must be a valid email address.");

        RuleFor(x => x.Password).NotEmpty();
    }
}
