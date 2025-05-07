using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.Services;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class LoginHandler:IRequestHandler<LoginCommand,Result<LoginCommandResponse>>
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IJwtProvider _jwtProvider;

    public LoginHandler(UserManager<AppUser> userManager
    ,SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanici bulunamadi.");
        }

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            
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

        var token = await _jwtProvider.CreateTokenAsync(user, cancellationToken);
        
        var response = new LoginCommandResponse()
        {
            AccessToken = token
        };

        return response;
    }
}