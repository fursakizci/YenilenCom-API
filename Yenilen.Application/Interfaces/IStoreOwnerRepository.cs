using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStoreOwnerRepository : IGenericRepository<StoreOwner>
{
    Task<bool> StoreOwnerExistsByPhoneNumberAsync(string phoneNumber = "", string email = "");
    Task<StoreOwner?> GetStoreIdByStoreOwnerIdAsync(Guid? appUserId, CancellationToken cancellationToken);
}