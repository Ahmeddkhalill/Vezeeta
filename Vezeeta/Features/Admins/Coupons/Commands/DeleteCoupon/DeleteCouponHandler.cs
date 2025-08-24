namespace Vezeeta.Features.Admins.Coupons.Commands.DeleteCoupon;

public class DeleteCouponHandler(ApplicationDbContext context) : IRequestHandler<DeleteCouponRequest, Result>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> Handle(DeleteCouponRequest request, CancellationToken cancellationToken)
    {
        var coupon = await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (coupon is null)
            return Result.Failure(CouponErrors.NotFound);

        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}