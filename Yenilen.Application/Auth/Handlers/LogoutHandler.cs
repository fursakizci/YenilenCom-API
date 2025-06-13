using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class LogoutHandler : IRequestHandler<LogoutCommand, Result<LogoutCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRequestContextService _requestContextService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(UserManager<AppUser> userManager,
        IRequestContextService requestContextService,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _requestContextService = requestContextService;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LogoutCommandResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();
        
        if (appUserId == Guid.Empty)
        {
            return Result<LogoutCommandResponse>.Failure("Kullanıcı bilgisi alınamadı");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _jwtProvider.ExpireAuthTokenCookie("ACCESS_TOKEN");
        _jwtProvider.ExpireAuthTokenCookie("REFRESH_TOKEN");

        return Result<LogoutCommandResponse>.Succeed(new LogoutCommandResponse());

    }
}