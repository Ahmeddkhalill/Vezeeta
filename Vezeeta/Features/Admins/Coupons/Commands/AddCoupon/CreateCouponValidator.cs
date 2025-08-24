namespace Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;

public class CreateCouponValidator : AbstractValidator<CreateCouponRequest>
{
    public CreateCouponValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be in the future.");
    }
}
