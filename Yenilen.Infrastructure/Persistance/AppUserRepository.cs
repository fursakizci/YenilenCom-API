using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class AppUserRepository : GenericRepository<AppUser,AppDbContext>, IAppUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<AppUser> _dbSet;
    
    public AppUserRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<AppUser>();
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        var user = await _dbSet.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }
}