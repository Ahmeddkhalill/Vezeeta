namespace Vezeeta.Features.Admins.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorRequest>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.DoctorId).GreaterThan(0);

        RuleFor(x => x.FirstName)
            .NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10,11}$")
            .WithMessage("Phone number must be 10 or 11 digits.");

        RuleFor(x => x.SpecializationId).GreaterThan(0);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(BeValidBirthDate)
            .WithMessage("Birth date must be a valid past date.");
    }

    private bool BeValidBirthDate(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return dateOfBirth != default && dateOfBirth <= today;
    }
}