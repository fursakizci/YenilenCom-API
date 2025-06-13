using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Search.Queries;
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

    [HttpGet("search-mapbox")]
    public async Task<IActionResult> GetStoresByDistance([FromQuery] GetStoresByDistanceQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("searchInput")]
    public async Task<IActionResult> SearchInputForStoresAndTags([FromQuery] GetStoresAndTagsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("searchBar-justCertainField")]
    public async Task<IActionResult> GetStoresFromSearchBar([FromQuery] GetStoresFromSearchBarQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("stores")]
    public async Task<IActionResult> GetStores([FromQuery] GetStoresQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
}