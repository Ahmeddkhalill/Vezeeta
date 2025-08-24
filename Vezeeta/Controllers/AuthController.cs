using Microsoft.Extensions.Localization;

namespace Vezeeta.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IStringLocalizer localizer) : BaseApiController
{
    private readonly IStringLocalizer _localizer = localizer;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] Features.Authentication.Commands.Register.RegisterRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(Features.Authentication.Queries.Login.LoginRequest request)
    {
        var authResult = await Mediator.Send(request);
        return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
    }

    [HttpGet("hello")]
    public IActionResult Hello()
    {
        var message = _localizer["Welcome"];
        return Ok(message);
    }
}
