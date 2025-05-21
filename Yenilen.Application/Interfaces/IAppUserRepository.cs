using Yenilen.Domain.Users;

namespace Yenilen.Application.Interfaces;

public interface IAppUserRepository : IGenericRepository<AppUser>
{
    Task<AppUser?> GetUserByEmailAsync(string email);
}