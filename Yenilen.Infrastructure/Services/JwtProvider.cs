using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yenilen.Application.Services;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.Options;

namespace Yenilen.Infrastructure.Services;

internal sealed class JwtProvider(IOptions<JwtOptions> options,  IHttpContextAccessor httpContextAccessor) : IJwtProvider
{
    public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        List<Claim> claims = new()
        {
            new Claim("user-id", user.Id.ToString())
        };

        var expires = options.Value.ExpirationTimeInHour;
        
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);
        
        JwtSecurityToken securityToken = new(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddHours(expires),
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(securityToken);

        return Task.FromResult(token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token)
    {
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName,token,
            new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddHours(options.Value.ExpirationTimeInHour),
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
    }
}