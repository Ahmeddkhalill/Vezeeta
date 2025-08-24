namespace Vezeeta.Features.Admins.Coupons.Commands.UpdateCoupon;

public class UpdateCouponValidator : AbstractValidator<UpdateCouponRequest>
{
    public UpdateCouponValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage("Coupon code is required.")
            .MaximumLength(50)
            .WithMessage("Coupon code must not exceed 50 characters.");

        RuleFor(c => c.Level)
            .IsInEnum()
            .WithMessage("Invalid discount level specified.");

        RuleFor(c => c.ExpiryDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Expiry date must be in the future.");
    }
}

