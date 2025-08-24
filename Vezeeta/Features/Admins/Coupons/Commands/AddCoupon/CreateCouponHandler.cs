namespace Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;

public class CreateCouponHandler(ApplicationDbContext context)
        : IRequestHandler<CreateCouponRequest, Result>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> Handle(CreateCouponRequest request, CancellationToken cancellationToken)
    {

        var exists = await _context.Coupons
            .AnyAsync(c => c.Code == request.Code, cancellationToken);

        if (exists)
            return Result.Failure<int>(CouponErrors.DuplicatedCode);

        var coupon = new Coupon
        {
            Code = request.Code,
            Level = request.Level,
            ExpiryDate = request.ExpiryDate,
            IsActive = true
        };

        _context.Coupons.Add(coupon);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}