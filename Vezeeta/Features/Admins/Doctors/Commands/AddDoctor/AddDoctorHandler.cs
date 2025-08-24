namespace Vezeeta.Features.Admins.Doctors.Commands.AddDoctor;

public class AddDoctorHandler(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context,
    IWebHostEnvironment env) : IRequestHandler<AddDoctorRequest, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IWebHostEnvironment _env = env;

    public async Task<Result> Handle(AddDoctorRequest request, CancellationToken cancellationToken)
    {
        var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var specialization = await _context.Specializations
            .FirstOrDefaultAsync(s => s.Id == request.SpecializationId, cancellationToken);

        if (specialization is null)
            return Result.Failure(SpecializationErrors.NotFound);

        string? imagePath = null;
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

            imagePath = $"/images/doctors/{fileName}";
        }

        var doctorUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            Image = request.Image.FileName
        };

        var result = await _userManager.CreateAsync(doctorUser, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(doctorUser, "Doctor");
            var doctor = new Doctor
            {
                UserId = doctorUser.Id,
                SpecializationId = request.SpecializationId
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
