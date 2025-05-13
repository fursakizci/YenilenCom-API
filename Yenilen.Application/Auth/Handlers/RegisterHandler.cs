using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Auth;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class RegisterHandler: IRequestHandler<RegisterCommand,Result<RegisterCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterHandler(UserManager<AppUser> userManager,
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        AppUser appUser = new()
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var identityResult = await _userManager.CreateAsync(appUser, request.Password);

        if (!identityResult.Succeeded)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<RegisterCommandResponse>.Failure("AppUser kaydi basarisiz.");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            AppUserId = appUser.Id
        };

        await _userRepository.AddUserAsync(user, cancellationToken);

        try
        {
            var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(appUser);
            await _refreshTokenRepository.AddAsync(refreshToken);
            
            _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", accessToken);
            _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token);
            
            await _unitOfWork.SaveChangesAsync(appUser.Id,cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<RegisterCommandResponse>.Failure("Refresh token kaydı başarısız.");
        }
        
       
        
        return Result<RegisterCommandResponse>.Succeed(new RegisterCommandResponse
        {
            UserId = user.Id
        });
    }
}