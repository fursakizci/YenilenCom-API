using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Auth;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

public class RefreshHandler:IRequestHandler<RefreshCommand,Result<RefreshCommandResponse>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly ITokenService _tokenService;
    private readonly IRequestContextService _requestContextService;
    private readonly IUnitOfWork _unitOfWork;
    
    public RefreshHandler(IRefreshTokenRepository refreshTokenRepository,
        UserManager<AppUser> userManager,
        IJwtProvider jwtProvider,
        ITokenService tokenService,
        IRequestContextService requestContextService,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _tokenService = tokenService;
        _requestContextService = requestContextService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<RefreshCommandResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var tokenInDb = await _refreshTokenRepository.GetByToken(request.RefreshToken);
        
        if(tokenInDb is null  || tokenInDb.IsRevoked == true)
            return Result<RefreshCommandResponse>.Failure("Gecersiz refresh token.");
        
        if (!tokenInDb.IsActive)
            return Result<RefreshCommandResponse>.Failure("Refresh token geçersiz veya süresi dolmuş.");
        
        var appUser = await _userManager.FindByIdAsync(tokenInDb.AppUserId.ToString());
        
        if (appUser is null || !appUser.IsActive || appUser.IsDeleted)
            return Result<RefreshCommandResponse>.Failure("Kullanıcı bulunamadı veya devre dışı.");
        
        var currentIp = _requestContextService.GetUserIpAddress();
        
        if (tokenInDb.CreatedByIp != currentIp)
            return Result<RefreshCommandResponse>.Failure("Şüpheli erişim: token kullanılamaz.");

        var (accessToken, refreshToken) = await _tokenService.RevokeAndRotateAsync(tokenInDb,appUser);
        
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(appUser.Id,cancellationToken);

        var response = new RefreshCommandResponse()
        {
            FullName = appUser.FullName,
            Email = appUser.Email
        };
        
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", accessToken);
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token);
        
        return response;
    }
}