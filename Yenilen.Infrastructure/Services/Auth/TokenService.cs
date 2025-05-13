using Microsoft.Extensions.Options;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Auth;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.Options;

namespace Yenilen.Infrastructure.Services.Auth;

public class TokenService:ITokenService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRequestContextService _requestContextService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOptions<JwtOptions> _options;
    
    public TokenService(IJwtProvider jwtProvider,
        IRequestContextService requestContextService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IOptions<JwtOptions> options)
    {
        _jwtProvider = jwtProvider;
        _requestContextService = requestContextService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _options = options;
    }
    
    public async Task<(string accessToken, RefreshToken refreshToken)> GenerateTokensAsync(AppUser user)
    {
        var accessToken = await _jwtProvider.CreateTokenAsync(user);
        var refreshToken = _jwtProvider.GenerateRefreshToken();
        
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            AppUserId = user.Id,
            AppUser = user,
            Expires = DateTime.UtcNow.AddDays(_options.Value.RefreshTokenLifetimeDays),
            CreatedByIp = _requestContextService.GetUserIpAddress()
        };
        
        return (accessToken, refreshTokenEntity);
    }
    
    public async Task<(string accessToken, RefreshToken refreshToken)> RevokeAndRotateAsync(RefreshToken token, AppUser user)
    {
        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        _refreshTokenRepository.Update(token);

        return await GenerateTokensAsync(user);
    }
}