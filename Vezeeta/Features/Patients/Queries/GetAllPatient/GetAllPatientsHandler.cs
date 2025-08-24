using Vezeeta.Features.Patients.Models;
using Vezeeta.Features.Patients.Queries.GetAllPatient;

namespace Vezeeta.Features.Admins.Queries.GetAllPatients;

public class GetAllPatientsHandler(ApplicationDbContext context) : IRequestHandler<GetAllPatientsRequest, IEnumerable<PatientResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PatientResponse>> Handle(GetAllPatientsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Include(p => p.User)
            .Select(p => new PatientResponse(
                p.User.Image,
                $"{p.User.FirstName} {p.User.LastName}",
                p.User.Email!,
                p.User.PhoneNumber!,
                p.User.Gender,
                p.User.DateOfBirth
            ))
            .ToListAsync(cancellationToken);
    }
}
