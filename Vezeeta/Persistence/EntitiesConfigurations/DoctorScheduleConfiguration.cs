using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vezeeta.Persistence.EntitiesConfigurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.HasIndex( x => new { x.DoctorId, x.DayOfWeek }).IsUnique();
    }
}
