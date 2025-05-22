using MediatR;
using Microsoft.AspNetCore.Http;
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

internal sealed class RegisterHandler: IRequestHandler<RegisterCommand,Result<RegisterCommandResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStoreOwnerRepository _storeOwnerRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRequestContextService _requsetContextService;
    private readonly IStoreRepository _storeRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterHandler(UserManager<AppUser> userManager,
        ICustomerRepository customerRepository,
        IStoreOwnerRepository storeOwnerRepository,
        IStaffRepository staffRepository,
        RoleManager<AppRole> roleManager,
        IJwtProvider jwtProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IRequestContextService requestContextService,
        IStoreRepository storeRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _customerRepository = customerRepository;
        _storeOwnerRepository = storeOwnerRepository;
        _staffRepository = staffRepository;
        _roleManager = roleManager;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _requsetContextService = requestContextService;
        _storeRepository = storeRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<RegisterCommandResponse>.Failure("Bu email adresi zaten kullanımda.");
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var phoneExistsResult =
                    await CheckPhoneNumberExists(request.PhoneNumber, request.Role, cancellationToken);

                if (phoneExistsResult.IsSuccessful)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<RegisterCommandResponse>.Failure(phoneExistsResult.ErrorMessages);
                }
            }
            
            var role = await _roleManager.FindByNameAsync(request.Role);

            if (role is null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<RegisterCommandResponse>.Failure($"Girdiginiz role girdisine ait veri bulunmamaktadir.");
            }
            
            AppUser appUser = new()
            {
                UserName = request.Email, 
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                Role = role
            }; 
            
        var identityResult = await _userManager.CreateAsync(appUser, request.Password);

        if (!identityResult.Succeeded)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            return Result<RegisterCommandResponse>.Failure($"AppUser kaydi basarisiz.{errors}");
        }
        
        await CreateRoleSpecificEntity(request, appUser.Id, cancellationToken);
        
       
        var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(appUser);
        await _refreshTokenRepository.AddAsync(refreshToken);
            
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", accessToken);
        _jwtProvider.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", refreshToken.Token);
            
        await _unitOfWork.SaveChangesAsync(appUser.Id,cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
           return Result<RegisterCommandResponse>.Succeed(new RegisterCommandResponse
           {
                UserId = appUser.Id.ToString(),
                Role = request.Role
            }); 
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<RegisterCommandResponse>.Failure($"Kullanici kayit islemi başarısız: {ex.Message}");
        }
    }

    private async Task<Result<bool>> CheckPhoneNumberExists(string phoneNumber, string role,
        CancellationToken cancellationToken)
    {
        switch (role)
        {
            case RoleNames.Customer:
                var isCustomerExist = await _customerRepository.CustomerExistsByPhoneNumberAsync(phoneNumber);
                if (isCustomerExist != null)
                    return Result<bool>.Failure("Bu telefon numarası zaten bir müşteri tarafından kullanılıyor.");
                break;
            
            case RoleNames.StoreOwner:
                var isStoreOwnerExist = await _storeOwnerRepository.StoreOwnerExistsByPhoneNumberAsync(phoneNumber);
                if (isStoreOwnerExist != null)
                    return Result<bool>.Failure("Bu telefon numarası zaten bir mağaza sahibi tarafından kullanılıyor.");
                break;
            
            case RoleNames.Staff:
                var isStaffExist = await _staffRepository.StaffExistsByPhoneNumberAsync(phoneNumber);
                if (isStaffExist != null)
                    return Result<bool>.Failure("Bu telefon numarası zaten bir çalışan tarafından kullanılıyor.");
                break;
            
            default:
                return Result<bool>.Failure("Geçersiz role.");
        }
        return Result<bool>.Succeed(true);
    }

    private async Task<Result<int>> CreateRoleSpecificEntity(RegisterCommand request, Guid appUserId,
        CancellationToken cancellationToken)
    {
        switch (request.Role)
        {
            case RoleNames.Customer:
                var customer = new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    AppUserId = appUserId
                };

                await _customerRepository.AddAsync(customer, cancellationToken);
                return customer.Id;
            
            case RoleNames.StoreOwner:
                var storeOwner = new StoreOwner()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    CompanyName = request.CompanyName,
                    AppUserId = appUserId
                };

                await _storeOwnerRepository.AddAsync(storeOwner, cancellationToken);
                return storeOwner.Id;
            
            case RoleNames.Staff:

                var storeOwnerAppUserId = _requsetContextService.GetCurrentUserId();
                
                if (storeOwnerAppUserId == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure("Staff rolü için geçerli magaza sahibi olarak giris yapmalisiz.");
                }

                var store =
                    await _storeRepository.GetStoreByUserIdAsync(storeOwnerAppUserId);
                
                if (store == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Failure("Calisan için geçerli magaza bulunamadi."); 
                }
                
                var staff = new Staff()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    StoreId = store.Id,
                    AppUserId = appUserId
                };

                await _staffRepository.AddAsync(staff, cancellationToken);
                return staff.Id;
            
            default:
                return Result<int>.Failure($"Desteklenmeyen rol: {request.Role}");
        }
    }
}