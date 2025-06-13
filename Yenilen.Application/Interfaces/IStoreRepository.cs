using Yenilen.Application.DTOs;
using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStoreRepository:IGenericRepository<Store>
{
    Task<string> GetStoreFullAddressById(int id);
    Task<StoreDashboardDto> GetStoreWithDetailsAsync(int id);
    Task<Store> GetStoreWithWorkingTimesAsync(int id);
    Task<Store?> GetStoreByUserIdAsync(Guid? appUserId);
    Task<Store?> GetStoreWithCategoriesByUserId(Guid? appUserId, CancellationToken cancellationToken);
    Task<IEnumerable<StoreSearchDto>?> SearchStoresByNameAsync(string? query, int maxResutls, CancellationToken cancellationToken);
    Task<IEnumerable<StoreSearchResultDto>> SearchStoresAsync(int? tagId, double latitude, double longitude,
        DateTime? date, CancellationToken cancellationToken = default);
}