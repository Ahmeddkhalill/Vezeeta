using Vezeeta.Features.Patients.Commands.BookDoctor;
using Vezeeta.Features.Patients.Queries.GetAllPatient;

namespace Vezeeta.Controllers;

[Route("/[controller]")]
[ApiController]

public class PatientsController : BaseApiController
{
    [Authorize(Roles = "Patient")]
    [HttpPost("book")]
    public async Task<IActionResult> BookDoctor([FromBody] BookDoctorRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsFailure ? result.ToProblem() : Ok(result.Value);
    }

    [HttpGet("")]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> GetAllPatients()
    {
        var result = await Mediator.Send(new GetAllPatientsRequest());
        return Ok(result);
    }
}
