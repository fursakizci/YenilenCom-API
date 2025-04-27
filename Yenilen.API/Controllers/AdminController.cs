using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreCommand command)
    {
        var storeId = await _mediator.Send(command);
        return Ok(new { Id = storeId });
    }

    [HttpPost("create-staff")]
    public async Task<IActionResult> CreateStaff()
    {
        return Ok();
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateStore()
    {
        return Ok();
    }
}