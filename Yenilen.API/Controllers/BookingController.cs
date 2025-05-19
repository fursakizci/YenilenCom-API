using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Booking.Commands;
using Yenilen.Application.Features.Booking.Queries;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController:ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("availabilities")]
    public async Task<IActionResult> GetAvailableDates([FromQuery] GetAvailableSlotsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateOppointment([FromBody] CreateAppointmentCommand command)
    {
        var appointmentId = await _mediator.Send(command);
        return Ok(new { Id = appointmentId });
    }

    [HttpGet("staff-availabilities")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] GetAvailableSlutsForStaffQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}