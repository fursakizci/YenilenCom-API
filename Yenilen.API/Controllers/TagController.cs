using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Tag.Handler;
using Yenilen.Application.Features.Tag.Queries;

namespace Yenilen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController:ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        var result = await _mediator.Send(new GetAllTagsQuery());
        return Ok(result);
    }
}