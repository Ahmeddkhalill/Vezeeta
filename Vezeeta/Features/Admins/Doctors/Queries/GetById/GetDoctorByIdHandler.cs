using Vezeeta.Features.Admins.Doctors.Models;

namespace Vezeeta.Features.Admins.Doctors.Queries.GetById;

public class GetDoctorByIdHandler(ApplicationDbContext context)
    : IRequestHandler<GetDoctorByIdRequest, DoctorDetailsResponse?>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<DoctorDetailsResponse?> Handle(GetDoctorByIdRequest request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialization)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null || doctor.User is null) 
            throw new Exception("Doctor not found");

        return new DoctorDetailsResponse(
            doctor.Id,
            doctor.User.Image ?? string.Empty,
            doctor.User.FirstName + " " + doctor.User.LastName,
            doctor.User.Email ?? string.Empty,
            doctor.User.PhoneNumber ?? string.Empty,
            doctor.Specialization?.Name ?? "",
            doctor.User.Gender.ToString(),
            doctor.User.DateOfBirth
        );
    }
}