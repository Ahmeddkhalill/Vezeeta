namespace Vezeeta.Features.Doctors.Commands.UpdateDoctorSchedule;

public class UpdateDoctorScheduleValidator : AbstractValidator<UpdateDoctorScheduleRequest>
{
    public UpdateDoctorScheduleValidator()
    {
        //RuleFor(x => x.ScheduleId)
        //    .GreaterThan(0)
        //    .WithMessage("ScheduleId must be greater than zero.");

        RuleFor(x => x.TimeSlots)
            .NotEmpty()
            .WithMessage("At least one time slot is required.");

        RuleForEach(x => x.TimeSlots)
            .SetValidator(new UpdateDoctorTimeSlotDtoValidator());
    }
}

public class UpdateDoctorTimeSlotDtoValidator : AbstractValidator<UpdateDoctorTimeSlotDto>
{
    public UpdateDoctorTimeSlotDtoValidator()
    {
        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime)
            .WithMessage("StartTime must be before EndTime.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}
