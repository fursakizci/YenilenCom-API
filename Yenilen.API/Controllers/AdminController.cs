using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yenilen.API.Auth;
using Yenilen.Application.Features.Category.Commands;
using Yenilen.Application.Features.Service.Commands;
using Yenilen.Application.Features.StaffMember.Commands;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Features.Store.Commands;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdminController:ControllerBase
{
    private readonly IMediator _mediator;
    
    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("store")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("staff/create")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
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

    [HttpPut("update}")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
    public async Task<IActionResult> UpdateStore()
    {
        return Ok();
    }

    [HttpPut("update/opening-times")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
    public async Task<IActionResult> UpdateStoreOpeninTimes([FromBody] UpdateStoreOpeningTimesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("category")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
    public async Task<IActionResult> CreateCategoryByStoreId([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("service")]
    [Authorize(Policy = PolicyNames.RequireStoreOwner)]
    public async Task<IActionResult> CreateServiceByCategoryId([FromBody] CreateServiceCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("staff/appointments")]
    [Authorize(Policy = PolicyNames.RequireStoreOwnerOrStaff)]
    public async Task<IActionResult> GetStaffAppointments([FromQuery] GetStaffAppointmentsByStaffIdQuery query )
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}