using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("dates")]
    public async Task<IActionResult> GetAvailableDates()
    {
        return Ok();
    }
    
    [HttpGet("times")]
    public async Task<IActionResult> GetAvailableTimes()
    {
        return Ok();
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateOppointment()
    {
        return Ok(new { Id = 0 });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAppointment([FromQuery] GetAvailableSlutsForStaffQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}