namespace Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;

public record CreateCouponRequest(
        string Code,
        DiscountLevel Level,
        DateTime ExpiryDate
    ) : IRequest<Result>;
