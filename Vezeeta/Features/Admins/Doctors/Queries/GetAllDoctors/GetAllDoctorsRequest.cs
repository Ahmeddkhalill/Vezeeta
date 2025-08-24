using Vezeeta.Features.Admins.Doctors.Models;

namespace Vezeeta.Features.Admins.Doctors.Queries.GetAllDoctors;

public record GetAllDoctorsRequest : IRequest<IEnumerable<DoctorResponse>>;