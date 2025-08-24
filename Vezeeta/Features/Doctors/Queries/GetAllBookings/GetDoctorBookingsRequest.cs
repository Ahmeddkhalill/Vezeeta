using Vezeeta.Features.Doctors.Models;

namespace Vezeeta.Features.Doctors.Queries.GetAllBookings;

public record GetDoctorBookingsRequest() : IRequest<IEnumerable<DoctorBookingResponse>>;