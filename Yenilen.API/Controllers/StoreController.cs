using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("store/{id}")]
    public async Task<IActionResult> GetStoreById(int Id)
    {
        return Ok();
    }

    [HttpGet("Stores")]
    public async Task<IActionResult> GetStoresByDistance()
    {
        return Ok();
    }
}