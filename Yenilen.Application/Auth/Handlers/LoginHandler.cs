using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Auth;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class LoginHandler:IRequestHandler<LoginCommand,Result<LoginCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(UserManager<AppUser> userManager
    ,SignInManager<AppUser> signInManager,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtProvider jwtProvider,
    ITokenService tokenService,
    IUnitOfWork unitOfWork
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtProvider = jwtProvider;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanici bulunamadi.");
        }

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = appUser.LockoutEnd - DateTime.UtcNow;
            
            if (timeSpan is not null)
                return (500, $"Şifrenizi 5 defa yanlış girdiğiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir");
            else
                return (500, "Kullanıcınız 5 kez yanlış şifre girdiği için 5 dakika süreyle bloke edilmiştir");
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Mail adresiniz onaylı değil");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Şifreniz yanlış");
        }

        var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(appUser);
        
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(appUser.Id,cancellationToken);
        
        var response = new LoginCommandResponse()
        {
            FullName = appUser.FullName,
            Email = appUser.Email
        };
        
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", accessToken);
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token);

        return response;
    }
}