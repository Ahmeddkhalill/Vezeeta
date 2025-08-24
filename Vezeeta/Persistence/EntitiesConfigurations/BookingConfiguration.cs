using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vezeeta.Entities;

namespace Vezeeta.Persistence.EntitiesConfigurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(b => b.Doctor)
            .WithMany()
            .HasForeignKey(b => b.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Patient)
            .WithMany()
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.DoctorSchedule)
            .WithMany()
            .HasForeignKey(b => b.DoctorScheduleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.DoctorTimeSlot)
            .WithMany()
            .HasForeignKey(b => b.DoctorTimeSlotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(b => b.CouponCode)
            .HasMaxLength(50);
    }
}
