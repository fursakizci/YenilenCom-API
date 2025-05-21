using TS.Result;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Services.Common;

public sealed class StoreOwnerPolicyService : IStoreOwnerPolicyService
{
    private readonly IRequestContextService _requestContextService;
    private readonly IStoreRepository _storeRepository;

    public StoreOwnerPolicyService(
        IRequestContextService requestContextService,
        IStoreRepository storeRepository
        )
    {
        _requestContextService = requestContextService;
        _storeRepository = storeRepository;
    }
    
    public Task<Result<Guid>> ValidateAndGetAppUserIdAsync(CancellationToken cancellationToken)
    {
        var userId = _requestContextService.GetCurrentUserId();

        return userId == null
            ? Task.FromResult(Result<Guid>.Failure("Bu kullanici bulunamadi."))
            : Task.FromResult(Result<Guid>.Succeed(userId.Value));
    }

    public async Task<Result<Store>> ValidateAndGetStoreAsync(Guid appUserId, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetStoreByUserIdAsync(appUserId);

        if (store != null)
        {
            return Result<Store>.Failure("Bu kullanici adina magaza kaydi bulunmaktadir.");
        }

        return Result<Store>.Succeed(store);
    }
}