using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yenilen.Application.Features.Booking.Queries;

namespace Yenilen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController:ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("categories/{storeId}")]
    public async Task<IActionResult> GetCategories(int storeId)
    {
        var categories = await _mediator.Send(new GetCategoriesByStoreIdQuery(storeId));
        if (categories == null) return NotFound(new { message = "Kategorilere ulasilamiyor." });
        return Ok(categories);
    }
    
    [HttpGet("services/{categoryId}")]
    public async Task<IActionResult> GetServices(int categoryId)
    {
        var services = await _mediator.Send(new GetServicesByCategoryIdQuery(categoryId));
        if (services == null) return NotFound(new { message = "Servislere ulasilamiyor." });
        return Ok(services);
    }
    
    [HttpGet("staff/{storeId}")]
    public async Task<IActionResult> GetStaffMembers(int storeId)
    {
        var staffMembers = await _mediator.Send(new GetStaffMembersByStoryIdQuery(storeId));
        if (staffMembers == null) return NotFound(new { message = "Servislere ulasilamiyor." });
        return Ok(staffMembers);
    }
    
    [HttpGet("dates")]
    public async Task<IActionResult> GetAvailableDates()
    {
        return Ok();
    }
    
    [HttpGet("times")]
    public async Task<IActionResult> GetAvailableTimes()
    {
        return Ok();
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateOppointment()
    {
        return Ok(new { Id = 0 });
    }
}