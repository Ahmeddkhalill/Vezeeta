namespace Vezeeta.Features.Admins.Coupons.Commands.UpdateCoupon;

public class UpdateCouponHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateCouponRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateCouponRequest request, CancellationToken cancellationToken)
    {
        var idString = _httpContextAccessor.HttpContext?.Request.RouteValues["id"]?.ToString();

        if (!int.TryParse(idString, out var id))
            return Result.Failure(CouponErrors.NotFound);

        var coupon = await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (coupon is null)
            return Result.Failure(CouponErrors.NotFound);

        coupon.Code = request.Code;
        coupon.Level = request.Level;
        coupon.ExpiryDate = request.ExpiryDate;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
