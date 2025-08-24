namespace Vezeeta.Entities;

public class Booking
{
    public int Id { get; set; }

    public int DoctorId { get; set; }
    public int DoctorScheduleId { get; set; }
    public int DoctorTimeSlotId { get; set; }
    public string PatientId { get; set; } = default!;
    public bool IsConfirmed { get; set; } = false;
    [Precision(18, 2)]
    public decimal OriginalPrice { get; set; }

    [Precision(18, 2)]
    public decimal FinalPrice { get; set; }

    public string? CouponCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public Doctor? Doctor { get; set; }
    public DoctorSchedule? DoctorSchedule { get; set; }
    public DoctorTimeSlot? DoctorTimeSlot { get; set; }
    public ApplicationUser? Patient { get; set; }
}
