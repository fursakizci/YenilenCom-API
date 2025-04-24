using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Profile.Queries;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Features.Users.Commands;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController:ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _mediator.Send(new GetByIdUserQuery(id));
        if (user == null) return NotFound(new { message = "Kullanici Bulunamadi." });
        return Ok(user);
    }
    
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfileByUserId(int id)
    {
        var user = await _mediator.Send(new GetProfileByIdQuery(id));
        if (user == null) return NotFound(new { message = "Kullanici Bulunamadi." });
        return Ok(user);
    }
    
    [HttpGet("favourite/{id}")]
    public async Task<IActionResult> GetFavouritesByUserId(int id)
    {
        var user = await _mediator.Send(new GetAllFavouritesByUserIdQuery(id));
        if (user == null) return NotFound(new { message = "Kullanici Bulunamadi." });
        return Ok(user);
    }
    
    [HttpGet("appointments/{id}")]
    public async Task<IActionResult> GetAppointmentsByUserId(int id)
    {
        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { Id = userId });
    }

    [HttpPost("{id}/address")]
    public async Task<IActionResult> UserAddAddress([FromBody] AddUserAddressCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new {Id = userId});
    }
    
    [HttpGet("addresses")]
    public async Task<IActionResult> UserGetAddresses()
    {
        return Ok();
    }
}