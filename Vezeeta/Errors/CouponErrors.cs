namespace Vezeeta.Errors;

public static class CouponErrors
{
     
 public static Error DuplicatedCode => new Error(
        "Coupon.DuplicatedCode",
        "Coupon code already exists with same Code.",
        StatusCodes.Status409Conflict
    );
    public static Error NotFound => new Error(
        "Coupon.NotFound",
        "Coupon not found.",
        StatusCodes.Status404NotFound
    );
    public static Error Expired => new Error(
        "Coupon.Expired",
        "Coupon has expired.",
        StatusCodes.Status400BadRequest
    );

}
