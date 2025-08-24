namespace Vezeeta.Features.Admins.Coupons.Commands.DeleteCoupon;

public record DeleteCouponRequest(int Id) : IRequest<Result>;