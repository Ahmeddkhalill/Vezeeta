namespace Vezeeta.Features.Patients.Commands.CancelBook;


public record CancelBookingRequest(int BookingId) : IRequest<Result>;