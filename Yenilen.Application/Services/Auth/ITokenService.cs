using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Services.Auth;

public interface ITokenService
{
    Task<(string accessToken, RefreshToken refreshToken)> GenerateTokensAsync(AppUser user);
    Task<(string accessToken, RefreshToken refreshToken)> RevokeAndRotateAsync(RefreshToken token, AppUser user);
}