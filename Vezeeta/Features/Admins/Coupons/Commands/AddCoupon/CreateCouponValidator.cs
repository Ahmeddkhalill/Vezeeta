namespace Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;

public class CreateCouponValidator : AbstractValidator<CreateCouponRequest>
{
    public CreateCouponValidator(IStringLocalizer<CreateCouponValidator> localizer)
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage(localizer["CodeRequired"])
            .MaximumLength(50).WithMessage(localizer["CodeMaxLength"]);

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.UtcNow).WithMessage(localizer["ExpiryDateFuture"]);
    }
}
