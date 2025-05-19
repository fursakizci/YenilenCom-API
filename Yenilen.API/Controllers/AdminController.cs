using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Category.Commands;
using Yenilen.Application.Features.Service.Commands;
using Yenilen.Application.Features.StaffMember.Commands;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Features.Store.Commands;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController:ControllerBase
{
    private readonly IMediator _mediator;
    
    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("store")]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("staff/create")]
    public async Task<IActionResult> CreateStaff([FromBody] CreateStaffMemberCommand command)
    {
        var staffId = await _mediator.Send(command);
        return Ok(new{ Id = staffId });
    }

    [HttpPost("staff/assign-working-times")]
    public async Task<IActionResult> AssignWorkingTimes()
    {
        return Ok();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateStore()
    {
        return Ok();
    }

    [HttpPut("update/opening-times")]
    public async Task<IActionResult> UpdateStoreOpeninTimes([FromBody] UpdateStoreOpeningTimesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("category")]
    public async Task<IActionResult> CreateCategoryByStoreId([FromBody] CreateCategoryCommand command)
    {
        var categoryId = await _mediator.Send(command);
        return Ok(new { Id = categoryId });
    }

    [HttpPost("service")]
    public async Task<IActionResult> CreateServiceByCategoryId([FromBody] CreateServiceCommand command)
    {
        var serviceId = await _mediator.Send(command);
        return Ok(new { Id = serviceId });
    }

    [HttpGet("staff/appointments")]
    public async Task<IActionResult> GetStaffAppointments([FromQuery] GetStaffAppointmentsByStaffIdQuery query )
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}