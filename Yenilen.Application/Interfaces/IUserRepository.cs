using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Interfaces;

public interface IUserRepository:IGenericRepository<User>
{
    Task<bool> IsExistsAsync(string phoneNumber, string email);
    Task<User?> GetByIdAsync(Guid? userId, bool includeRelated = false);
    Task<User?> GetFavouriteByIdAsync(int id);
    Task AddUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetUserByGuid(Guid? userId);
}