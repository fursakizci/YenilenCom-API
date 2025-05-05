using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Category.Queries;
using Yenilen.Application.Features.Service.Queries;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Features.Store.Queries;


namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController: ControllerBase
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetStoreById([FromQuery] GetStoreByIdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("staff-members")]
    public async Task<IActionResult> GetStaffMembers([FromQuery] GetStaffMembersByStoryIdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoriesByStoreId([FromQuery] GetCategoriesByStoreIdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("services")]
    public async Task<IActionResult> GetServicesByCategoryId([FromQuery] GetServicesByCategoryIdQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}