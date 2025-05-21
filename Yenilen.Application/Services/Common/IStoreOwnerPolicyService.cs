using TS.Result;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Services.Common;

public interface IStoreOwnerPolicyService
{
    Task<Result<Guid>> ValidateAndGetAppUserIdAsync(CancellationToken cancellationToken);
    Task<Result<Store>> ValidateAndGetStoreAsync(Guid appUserId, CancellationToken cancellationToken);
}