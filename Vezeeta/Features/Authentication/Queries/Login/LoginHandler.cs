using Vezeeta.Authentication;

namespace Vezeeta.Features.Authentication.Queries.Login;

public class LoginHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<LoginRequest, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var userRoles = await _userManager.GetRolesAsync(user);

        var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);

        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);

        return Result.Success(response);
    }
}
