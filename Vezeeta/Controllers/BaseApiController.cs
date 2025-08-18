using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
}
