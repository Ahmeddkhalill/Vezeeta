using Vezeeta.Features.Admins.Coupons.Commands.AddCoupon;
using Vezeeta.Features.Admins.Coupons.Commands.DeactivateCoupon;
using Vezeeta.Features.Admins.Coupons.Commands.DeleteCoupon;
using Vezeeta.Features.Admins.Coupons.Commands.UpdateCoupon;

namespace Vezeeta.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CouponsController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddCoupon([FromBody] CreateCouponRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        var result = await Mediator.Send(new DeleteCouponRequest(id));
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCoupon(int id, [FromBody] UpdateCouponRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> DeactivateCoupon(int id)
    {
        var result = await Mediator.Send(new DeactivateCouponRequest());
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
