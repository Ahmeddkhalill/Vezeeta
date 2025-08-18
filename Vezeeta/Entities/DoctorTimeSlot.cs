namespace Vezeeta.Entities;

public class DoctorTimeSlot
{
    public int Id { get; set; }
    public int DoctorScheduleId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    [Precision(18, 2)]
    public decimal Price { get; set; }
    public bool IsBooked { get; set; } = false;

    public DoctorSchedule? DoctorSchedule { get; set; }
}
