namespace Vezeeta.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public int SpecializationId { get; set; }
    public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = [];

    public ApplicationUser? User { get; set; }
    public Specialization? Specialization { get; set; }
}
