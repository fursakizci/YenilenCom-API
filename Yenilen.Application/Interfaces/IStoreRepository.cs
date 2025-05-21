using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStoreRepository:IGenericRepository<Store>
{
    Task<string> GetStoreFullAddressById(int id);
    Task<Store> GetStoreWithDetailsAsync(int id);
    Task<Store> GetStoreWithWorkingTimesAsync(int id);
    Task<Store?> GetStoreByUserIdAsync(Guid? appUserId);
    Task<Store?> GetStoreWithCategoriesByUserId(Guid? appUserId, CancellationToken cancellationToken);
}