namespace Vezeeta.Persistence.EntitiesConfigurations;

public class DoctorTimeSlotConfiguration : IEntityTypeConfiguration<DoctorTimeSlot>
{
    public void Configure(EntityTypeBuilder<DoctorTimeSlot> builder)
    {
        builder.HasIndex( x => new { x.DoctorScheduleId, x.StartTime, x.EndTime }).IsUnique();
    }
}
