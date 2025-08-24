namespace Vezeeta.Persistence.EntitiesConfigurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.Property(s => s.Date).HasColumnType("date");

        builder.HasIndex( x => new { x.DoctorId, x.DayOfWeek }).IsUnique();
    }
}
