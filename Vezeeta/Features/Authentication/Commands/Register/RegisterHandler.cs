using Vezeeta.Authentication;

namespace Vezeeta.Features.Authentication.Commands.Register;

public class RegisterHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider,
    IWebHostEnvironment env) : IRequestHandler<RegisterRequest, AuthResponse?>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IWebHostEnvironment _env = env;

    public async Task<AuthResponse?> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return null; 

        string? imagePath = null;
        if (request.Image is not null)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", "users");
            Directory.CreateDirectory(uploadsFolder); 

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
            Image = request.Image.FileName 
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return null;

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
    }
}
