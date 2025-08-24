namespace Vezeeta.Features.Admins.Coupons.Commands.UpdateCoupon;

public record UpdateCouponRequest(
    string Code,
    DiscountLevel Level,
    DateTime ExpiryDate
) : IRequest<Result>;