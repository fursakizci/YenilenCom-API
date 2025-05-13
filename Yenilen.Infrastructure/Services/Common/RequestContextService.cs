using Microsoft.AspNetCore.Http;
using Yenilen.Application.Services.Common;

namespace Yenilen.Infrastructure.Services.Common;

public class RequestContextService:IRequestContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentUserId()
    {
        Guid UserId = new Guid();
        
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("user-id")?.Value ?? string.Empty;
        
        if(Guid.TryParse(userIdClaim , out var parsedUserId))
        {
            UserId = parsedUserId;
        }
        else
        {
            UserId = Guid.Empty;
        }

        return UserId;
    }

    public string GetUserIpAddress()
    {
        return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
    }

}