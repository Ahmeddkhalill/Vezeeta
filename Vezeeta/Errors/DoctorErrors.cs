namespace Vezeeta.Errors;

public static class DoctorErrors
{
    public static readonly Error Unauthorized = new(
        "Doctor.Unauthorized",
        "You must be logged in as a doctor to perform this action.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error DoctorNotFound = new(
        "Doctor.NotFound",
        "Doctor not found.",
        StatusCodes.Status404NotFound);

    public static readonly Error ScheduleAlreadyExists = new(
        "Doctor.ScheduleAlreadyExists",
        "A schedule already exists for this date.",
        StatusCodes.Status409Conflict);

    public static readonly Error ScheduleNotFound = new(
        "Doctor.ScheduleNotFound",
        "Schedule not found.",
        StatusCodes.Status404NotFound);

    public static readonly Error ScheduleUpdateNotAllowed = new(
        "Doctor.ScheduleUpdateNotAllowed",
        "Cannot update a schedule that has booked time slots.",
        StatusCodes.Status400BadRequest);

    public static readonly Error ScheduleDeleteNotAllowed = new(
        "Doctor.ScheduleDeleteNotAllowed",
        "Cannot delete a schedule that has booked time slots.",
        StatusCodes.Status400BadRequest);
}

