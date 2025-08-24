namespace Vezeeta.Entities;

public class DoctorSchedule
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; } 
    public DayOfWeek DayOfWeek { get; set; }

    public Doctor? Doctor { get; set; }
    public ICollection<DoctorTimeSlot> TimeSlots { get; set; } = [];
}
public enum DayOfWeek
{
    Saturday = 0,
    Sunday = 1,
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6
}