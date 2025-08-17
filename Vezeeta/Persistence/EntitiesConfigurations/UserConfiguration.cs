using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vezeeta.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(50);

        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}
