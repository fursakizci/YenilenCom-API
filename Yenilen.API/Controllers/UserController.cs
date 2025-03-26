using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Application.Features.Users.Queries;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController:Controller
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

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { Id = userId });
    }
}