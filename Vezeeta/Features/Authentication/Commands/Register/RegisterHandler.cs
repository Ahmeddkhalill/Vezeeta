namespace Vezeeta.Features.Authentication.Commands.Register;

public class RegisterHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider,
    IWebHostEnvironment env, ApplicationDbContext context)
    : IRequestHandler<RegisterRequest, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IWebHostEnvironment _env = env;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<AuthResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure<AuthResponse>(UserErrors.DuplicatedEmail);

        string? imagePath = null;
        if (request.Image is not null)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "users"); Directory.CreateDirectory(uploadsFolder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream, cancellationToken);
            }

            imagePath = $"/images/users/{fileName}";
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            Image = imagePath
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Patient");
            var patient = new Patient
            {
                ApplicationUserId = user.Id,
                User = user
            };

            _context.Patients.Add(patient); await _context.SaveChangesAsync(cancellationToken);
            var userRoles = await _userManager.GetRolesAsync(user); var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);

            var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
            return Result.Success(response);

        }
        var error = result.Errors.First();
        return Result.Failure<AuthResponse>(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}