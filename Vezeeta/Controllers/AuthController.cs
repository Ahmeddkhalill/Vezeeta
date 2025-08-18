namespace Vezeeta.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    [HttpPost("register")]

    public async Task<IActionResult> Register([FromForm] Features.Authentication.Commands.Register.RegisterRequest request)
    {
        var result = await Mediator.Send(request);
        return result is null ? BadRequest("Registration failed. Please try again.") : Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(Features.Authentication.Queries.Login.LoginRequest request)
    {
        var authResult = await Mediator.Send(request);
        return authResult is null ? BadRequest("Invalid Email/Password") : Ok(authResult);
    }
}
