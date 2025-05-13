namespace Yenilen.Application.Services.Common;

public interface IRequestContextService
{
    Guid? GetCurrentUserId();
    string GetUserIpAddress();
}