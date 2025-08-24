namespace Vezeeta.Entities;

public class Coupon
{
    public int Id { get; set; }

    public string Code { get; set; } = default!; 

    public DiscountLevel Level { get; set; }     

    public DateTime ExpiryDate { get; set; }     

    public bool IsActive { get; set; } = true;   
}

public enum DiscountLevel
{
    TenPercent = 1,
    TwentyPercent = 2,
    ThirtyPercent = 3,
    FortyPercent = 4,
    FiftyPercent = 5,
    SixtyPercent = 6,
    SeventyPercent = 7,
    EightyPercent = 8,
    NinetyPercent = 9
}