using Microsoft.AspNetCore.Authorization;
using Vezeeta.Features.Admins.Doctors.Commands.AddDoctor;
using Vezeeta.Features.Admins.Doctors.Commands.DeleteDoctor;
using Vezeeta.Features.Admins.Doctors.Queries.GetAllDoctors;
using Vezeeta.Features.Admins.Doctors.Queries.GetById;
using Vezeeta.Features.Doctors.Commands.AddDoctorSchedule;
using Vezeeta.Features.Doctors.Commands.ConfirmBooking;
using Vezeeta.Features.Doctors.Commands.DeleteDoctorSchedule;
using Vezeeta.Features.Doctors.Commands.UpdateDoctorSchedule;
using Vezeeta.Features.Doctors.Queries.GetAllBookings;

namespace Vezeeta.Controllers;

[Route("[controller]")]
[ApiController]
public class DoctorsController : BaseApiController
{
    [HttpGet("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var result = await Mediator.Send(new GetAllDoctorsRequest());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDoctorById(int id)
    {
        var result = await Mediator.Send(new GetDoctorByIdRequest(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddDoctor([FromForm] AddDoctorRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromForm] UpdateDoctorRequest request)
    {
        var result = await Mediator.Send(request with { DoctorId = id });
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var result = await Mediator.Send(new DeleteDoctorRequest(id));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpPost("AddSchedule")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> AddSchedule([FromBody] AddDoctorScheduleRequest request)
    {
        var result = await Mediator.Send(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("UpdateSchedule/{scheduleId}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> UpdateSchedule([FromRoute] int scheduleId, [FromBody] UpdateDoctorScheduleRequest request)
    {
        var result = await Mediator.Send(new UpdateDoctorScheduleRequest(scheduleId, request.TimeSlots));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("DeleteSchedule/{scheduleId}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> DeleteSchedule([FromRoute] int scheduleId)
    {
        var result = await Mediator.Send(new DeleteDocterScheduleRequest(scheduleId));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("Bookings")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetBookings()
    {
        var result = await Mediator.Send(new GetDoctorBookingsRequest());
        return Ok(result);
    }

    [HttpPost("ConfirmBooking/{bookingId}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> ConfirmBooking([FromRoute] int bookingId)
    {
        var result = await Mediator.Send(new ConfirmBookingRequest(bookingId));
        return result.IsSuccess ? Ok("Booking confirmed successfully") : result.ToProblem();
    }
}
