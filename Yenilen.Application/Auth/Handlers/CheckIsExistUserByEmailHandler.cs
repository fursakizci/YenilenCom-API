using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;
using Yenilen.Application.Auth.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class CheckIsExistUserByEmailHandler : IRequestHandler<CheckIsExistUserByEmailQuery, Result<CheckIsExistUserByEmailQueryResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserRepository _appUserRepository;
    
    public CheckIsExistUserByEmailHandler(UserManager<AppUser> userManager,
        IAppUserRepository appUserRepository)
    {
        _userManager = userManager;
        _appUserRepository = appUserRepository;
    }
    
    public async Task<Result<CheckIsExistUserByEmailQueryResponse>> Handle(CheckIsExistUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var result = new CheckIsExistUserByEmailQueryResponse();

        if (user is null)
        {
            result.isEmailExist = false;
            return Result<CheckIsExistUserByEmailQueryResponse>.Succeed(result);
        };

        var userWithRole = await _appUserRepository.GetUserByEmailAsync(request.Email);

        if (userWithRole.Role.Name == RoleNames.Customer)
        {
            result.isEmailExist = true;
            return Result<CheckIsExistUserByEmailQueryResponse>.Succeed(result);
        }

        result.isEmailExist = false;
        return Result<CheckIsExistUserByEmailQueryResponse>.Succeed(result);
    }
}