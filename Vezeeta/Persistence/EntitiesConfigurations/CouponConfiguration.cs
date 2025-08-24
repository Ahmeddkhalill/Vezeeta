namespace Vezeeta.Persistence.EntitiesConfigurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.Property(c => c.Code).IsRequired();

        builder.Property(c => c.Level).HasConversion<int>() .IsRequired();

        builder.Property(c => c.ExpiryDate).IsRequired();
    }
}
