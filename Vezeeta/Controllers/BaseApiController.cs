namespace Vezeeta.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
}
