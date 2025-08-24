namespace Vezeeta.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? Image { get; set; }

    public int? SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }

}

public enum Gender
{
    Female = 0,
    Male = 1,
}