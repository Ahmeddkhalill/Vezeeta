namespace Vezeeta.Errors;

public static class BookingErrors
{
    public static readonly Error Unauthorized = new(
        "Booking.Unauthorized",
        "You must be logged in as a patient to perform this action.",
        StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidSchedule = new(
        "Booking.InvalidSchedule",
        "The specified schedule is invalid or does not exist.",
        StatusCodes.Status404NotFound);

    public static readonly Error InvalidTimeSlot = new(
        "Booking.InvalidTimeSlot",
        "The specified time slot is invalid or does not exist.",
        StatusCodes.Status404NotFound);

    public static readonly Error TimeSlotAlreadyBooked = new(
        "Booking.TimeSlotAlreadyBooked",
        "This time slot is already booked.",
        StatusCodes.Status409Conflict);

    public static readonly Error CouponNotFound = new(
        "Booking.CouponNotFound",
        "The specified coupon code is invalid or has expired.",
        StatusCodes.Status404NotFound);

    public static readonly Error NotFound = new(
        "Booking.NotFound",
        "The specified booking does not exist.",
        StatusCodes.Status404NotFound);

    public static readonly Error AlreadyConfirmed = new(
        "Booking.AlreadyConfirmed",
        "This booking has already been confirmed.",
        StatusCodes.Status409Conflict);

    public static readonly Error NotFoundDoctor = new(
        "Booking.NotFoundDoctor",
        "The specified doctor does not exist.",
        StatusCodes.Status404NotFound);
}
