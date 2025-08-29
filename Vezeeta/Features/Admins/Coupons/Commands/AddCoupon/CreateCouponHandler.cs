namespace Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;

public class CreateCouponHandler(ApplicationDbContext context, IStringLocalizer<CreateCouponHandler> localizer) : IRequestHandler<CreateCouponRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IStringLocalizer<CreateCouponHandler> _localizer = localizer;

    public async Task<Result> Handle(CreateCouponRequest request, CancellationToken cancellationToken)
    {
        var exists = await _context.Coupons
            .AnyAsync(c => c.Code == request.Code, cancellationToken);

        if (exists)
            return Result.Failure(new Error(CouponErrors.DuplicatedCode.Code,_localizer["DuplicatedCode"], CouponErrors.DuplicatedCode.statusCode));
        
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
