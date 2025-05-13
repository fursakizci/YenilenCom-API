using Yenilen.Domain.Users;

namespace Yenilen.Application.Services;

public interface IJwtProvider
{
    public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken = default);
    public string GenerateRefreshToken();

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token);
}