using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IServiceRepository:IGenericRepository<Service>
{
    Task<IEnumerable<Service>> GetServicesByCategoryIdAsync(int id);
    Task<bool> AllServicesExistAsync(List<int> serviceIds, CancellationToken cancellationToken = default);
    Task<IEnumerable<Service>> GetByIdsAsync(List<int> serviceIds, CancellationToken cancellationToken);
}