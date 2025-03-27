using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IUserRepository:IGenericRepository<User>
{
    Task<bool> IsExistsAsync(string phoneNumber, string email);
    Task<User?> GetByIdAsync(int id, bool includeRelated = false);
    Task<User?> GetFavouriteByIdAsync(int id);
}