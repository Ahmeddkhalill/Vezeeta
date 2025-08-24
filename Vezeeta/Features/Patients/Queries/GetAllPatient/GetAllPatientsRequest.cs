using Vezeeta.Features.Patients.Models;

namespace Vezeeta.Features.Patients.Queries.GetAllPatient;
public record GetAllPatientsRequest() : IRequest<IEnumerable<PatientResponse>>;