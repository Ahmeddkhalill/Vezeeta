using Vezeeta.Features.Admins.Doctors.Models;

namespace Vezeeta.Features.Admins.Doctors.Queries.GetById;

public record GetDoctorByIdRequest(int DoctorId) : IRequest<DoctorDetailsResponse?>;