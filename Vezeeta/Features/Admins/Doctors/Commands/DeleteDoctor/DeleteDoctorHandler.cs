namespace Vezeeta.Features.Admins.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorHandler(ApplicationDbContext context) : IRequestHandler<DeleteDoctorRequest, Result>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> Handle(DeleteDoctorRequest request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null || doctor.User is null)
            return Result.Failure(DoctorErrors.DoctorNotFound);

        _context.Users.Remove(doctor.User);
        _context.Doctors.Remove(doctor);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}