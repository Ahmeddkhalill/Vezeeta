namespace Vezeeta.Features.Patients.Queries.SearchDoctors;

public record GetAllDoctorsRequest
    : IRequest<Result<IEnumerable<Models.GetAllDoctorsResponse>>>;
