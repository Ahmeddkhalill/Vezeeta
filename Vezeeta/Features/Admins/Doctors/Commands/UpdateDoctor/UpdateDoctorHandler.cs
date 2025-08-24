namespace Vezeeta.Features.Admins.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorHandler(ApplicationDbContext context, IWebHostEnvironment env) : IRequestHandler<UpdateDoctorRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IWebHostEnvironment _env = env;

    public async Task<Result> Handle(UpdateDoctorRequest request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null || doctor.User is null)
            return Result.Failure(DoctorErrors.DoctorNotFound);

        doctor.User.FirstName = request.FirstName;
        doctor.User.LastName = request.LastName;
        doctor.User.PhoneNumber = request.PhoneNumber;
        doctor.User.DateOfBirth = request.DateOfBirth;
        doctor.SpecializationId = request.SpecializationId;

        if (request.Image is not null)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "doctors");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream, cancellationToken);
            }

            doctor.User.Image = $"/images/doctors/{fileName}";
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}