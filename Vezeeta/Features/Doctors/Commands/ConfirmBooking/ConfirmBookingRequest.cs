namespace Vezeeta.Features.Doctors.Commands.ConfirmBooking;

public record ConfirmBookingRequest(int BookingId) : IRequest<Result>;