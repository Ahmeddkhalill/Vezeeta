namespace Vezeeta.Features.Doctors.Commands.AddDoctorSchedule;

public class AddDoctorScheduleValidator : AbstractValidator<AddDoctorScheduleRequest>
{
    public AddDoctorScheduleValidator()
    {
        RuleFor(x => x.Days).NotEmpty();

        RuleForEach(x => x.Days).ChildRules(day =>
        {
            day.RuleFor(d => d.Date).NotEmpty();

            day.RuleFor(d => d.DayOfWeek)
               .IsInEnum();

            day.RuleFor(d => d)
               .Must(d => MapToEntitiesDayOfWeek(d.Date) == d.DayOfWeek)
               .WithMessage("Date does not match the provided DayOfWeek.");

            day.RuleFor(d => d.TimeSlots).NotEmpty();

            day.RuleForEach(d => d.TimeSlots).ChildRules(slot =>
            {
                slot.RuleFor(s => s.EndTime).GreaterThan(s => s.StartTime);
                slot.RuleFor(s => s.Price).GreaterThan(0);
            });
        });
    }

    private static Entities.DayOfWeek MapToEntitiesDayOfWeek(DateOnly date) =>
        date.DayOfWeek switch
        {
            System.DayOfWeek.Saturday => Entities.DayOfWeek.Saturday,
            System.DayOfWeek.Sunday   => Entities.DayOfWeek.Sunday,
            System.DayOfWeek.Monday   => Entities.DayOfWeek.Monday,
            System.DayOfWeek.Tuesday  => Entities.DayOfWeek.Tuesday,
            System.DayOfWeek.Wednesday=> Entities.DayOfWeek.Wednesday,
            System.DayOfWeek.Thursday => Entities.DayOfWeek.Thursday,
            System.DayOfWeek.Friday   => Entities.DayOfWeek.Friday,
            _ => throw new ArgumentOutOfRangeException()
        };
}
