namespace Vezeeta.Entities;

public class Patient
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}
