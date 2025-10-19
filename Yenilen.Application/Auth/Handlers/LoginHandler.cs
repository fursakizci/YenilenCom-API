using MediatR;
using Microsoft.AspNetCore.Identity;

using TS.Result;
using Yenilen.Application.Auth.Commands;
using Yenilen.Application.DTOs;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Auth;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class LoginHandler:IRequestHandler<LoginCommand,Result<LoginCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(UserManager<AppUser> userManager
    ,SignInManager<AppUser> signInManager,
    IAppUserRepository appUserRepository,
    ICustomerRepository customerRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtProvider jwtProvider,
    ITokenService tokenService,
    IUnitOfWork unitOfWork
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appUserRepository = appUserRepository;
        _customerRepository = customerRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtProvider = jwtProvider;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        var appUser = await _userManager.FindByEmailAsync(request.Email);
        
        if (appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("geçersiz email veya şifre.");
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
        
        var userRole = await _appUserRepository.GetUserByEmailAsync(request.Email);

        if (userRole is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcıya atanmış bir rol bulunamadı.");
        }

        var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(appUser);
        
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(appUser.Id,cancellationToken);

        var customer = await _customerRepository.GetCustomerWithAvatarAndAddressesAsync(appUser.Id, cancellationToken);
        
        if (customer is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadi.");
        }
        
        var response = new LoginCommandResponse()
        {
            Id = customer.Id.ToString(),
            FullName = appUser.FullName,
            Email = appUser.Email,
            AvatarUrl = customer.AvatarUrl?.ImageUrl ?? string.Empty,
            Addresses = customer?.Addresses != null 
                ? customer.Addresses.Select(a => new AddressDto
                {
                    Label = a.Label,
                    FullAddress = a.FullAddress,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    CountryCode = a.CountryCode,
                    Country = a.Country,
                    City = a.City,
                    District = a.District,
                    Region = a.Region,
                    PostCode = a.PostCode 
                }).ToList() : new List<AddressDto>()
        };
        
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", accessToken);
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token);

        return Result<LoginCommandResponse>.Succeed(response);
    }
}