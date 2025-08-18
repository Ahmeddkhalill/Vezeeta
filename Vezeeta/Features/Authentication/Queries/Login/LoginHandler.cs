using Vezeeta.Authentication;
using Vezeeta.Features.Authentication.Models;

namespace Vezeeta.Features.Authentication.Queries.Login;

public class LoginHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<LoginRequest, AuthResponse?>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<AuthResponse?> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return null;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
            return null;

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);
 
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
    }
}
