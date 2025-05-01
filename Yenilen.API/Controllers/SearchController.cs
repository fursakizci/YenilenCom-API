using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Features.Tag.Commands;
using Yenilen.Application.Features.Tag.Queries;

namespace Yenilen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController:ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetAllTags()
    {
        var result = await _mediator.Send(new GetAllTagsQuery());
        return Ok(result);
    }

    [HttpGet("store-by-distance")]
    public async Task<IActionResult> GetStoresByDistance([FromQuery] GetStoresByDistanceQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
    {
        var tagId = await _mediator.Send(command);
        return Ok(new {Id = tagId});
    }
}