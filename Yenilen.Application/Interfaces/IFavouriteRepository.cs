using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IFavouriteRepository:IGenericRepository<Favourite>
{
    Task<bool> ExistsAsync(int customerId, int storeId, CancellationToken cancellationToken);
}