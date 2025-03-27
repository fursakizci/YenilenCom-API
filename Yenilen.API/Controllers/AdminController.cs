using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}