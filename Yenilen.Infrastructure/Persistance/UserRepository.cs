using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _dbSet;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<User>();
    }

    public async Task<bool> IsExistsAsync(string phoneNumber, string email)
    {
        return await _dbSet.AnyAsync(i => i.Email == email || i.PhoneNumber == phoneNumber);
    }

    public async Task<User?> GetByIdAsync(int id, bool includeRelated = false)
    {
        if (!includeRelated)
        {
            return await _dbSet.FindAsync(id);
        }

        return await _dbSet
            .Include(u => u.Addresses)
            .Include(u => u.Image)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetFavouriteByIdAsync(int id)
    {
        return await _dbSet.Include(u => u.Favourites)
            .FirstOrDefaultAsync(u => u.Id == id);
        
    }
}