using Vezeeta.Features.Patients.Models;

namespace Vezeeta.Features.Patients.Queries.GetPatientById;

public record GetPatientByIdRequest(int PatientId) : IRequest<PatientDetailsResponse?>;