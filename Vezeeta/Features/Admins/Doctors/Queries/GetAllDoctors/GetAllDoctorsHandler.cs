using Vezeeta.Features.Admins.Doctors.Models;

namespace Vezeeta.Features.Admins.Doctors.Queries.GetAllDoctors;

public class GetAllDoctorsHandler(ApplicationDbContext context) : IRequestHandler<GetAllDoctorsRequest, IEnumerable<DoctorResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<DoctorResponse>> Handle(GetAllDoctorsRequest request, CancellationToken cancellationToken)
    {
        var doctors = await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialization)
            .Select(d => new DoctorResponse(
                d.Id,
                d.User!.Image ?? string.Empty,
                d.User.FirstName + " " + d.User.LastName,
                d.User.Email!,
                d.User.PhoneNumber!,
                d.Specialization != null ? d.Specialization.Name : "",
                d.User.Gender.ToString()
            ))
            .ToListAsync(cancellationToken);

        return doctors;
    }
}