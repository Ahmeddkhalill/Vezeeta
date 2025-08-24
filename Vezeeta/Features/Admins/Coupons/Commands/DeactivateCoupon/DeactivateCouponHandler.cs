namespace Vezeeta.Features.Admins.Coupons.Commands.DeactivateCoupon;

public class DeactivateCouponHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeactivateCouponRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(DeactivateCouponRequest request, CancellationToken cancellationToken)
    {
        var idString = _httpContextAccessor.HttpContext?.Request.RouteValues["id"]?.ToString();

        if (!int.TryParse(idString, out var id))
            return Result.Failure(CouponErrors.NotFound);

        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (coupon == null)
            return Result.Failure(CouponErrors.NotFound);

        coupon.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}