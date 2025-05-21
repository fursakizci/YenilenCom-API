using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yenilen.API.Auth;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Features.Users.Commands;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController:ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromQuery] GetByIdUserQuery query)
    {
        var result = await _mediator.Send(query);
        //if (user == null) return NotFound(new { message = "Kullanici Bulunamadi." });
        return Ok(result);
    }
    
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfileByUserId()
    {
        var response = await _mediator.Send(new GetProfileByIdQuery());
        return Ok(response);
    }
    
    [HttpGet("favourite/{id}")]
    public async Task<IActionResult> GetFavouritesByUserId([FromQuery] GetAllFavouritesByUserIdQuery query)
    {
        var result = await _mediator.Send(query);
        // if (user == null) return NotFound(new { message = "Kullanici Bulunamadi." });
        return Ok(result);
    }
    
    [HttpGet("appointments")]
    public async Task<IActionResult> GetAppointmentsByUserId([FromQuery] GetUserAppointmentsByUserIdQuery query)
    {
        var appointments = await _mediator.Send(query);
        return Ok(appointments);
    }

    // [HttpPost("create")]
    // public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    // {
    //     var result = await _mediator.Send(command);
    //     return Ok(result);
    // }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("address")]
    public async Task<IActionResult> UserAddAddress([FromBody] AddUserAddressCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("favourite")]
    public async Task<IActionResult> AddFavouriteStore([FromBody] AddFavouriteStoreCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}