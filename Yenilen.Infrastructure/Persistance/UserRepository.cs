using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _dbSet;
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(AppDbContext context, UserManager<AppUser> userManager) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<User>();
        _userManager = userManager;
    }

    public async Task<bool> IsExistsAsync(string phoneNumber, string email)
    {
        return await _dbSet.AnyAsync(i => i.Email == email || i.PhoneNumber == phoneNumber);
    }

    public async Task<User?> GetByIdAsync(Guid? userId, bool includeRelated = false)
    {
        if (!includeRelated)
        {
            return await _dbSet.FindAsync(userId);
        }
        
        if (userId == Guid.Empty || userId is null)
            return new User();
        
        return await _dbSet
            .Include(u => u.Addresses)
            .Include(u => u.AvatarUrl)
            .FirstOrDefaultAsync(u => u.AppUserId == userId);
    }

    public async Task<User?> GetFavouriteByIdAsync(int id)
    {
        return await _dbSet.Include(u => u.Favourites)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetUserByGuid(Guid? userId)
    {
        return await _dbSet.Where(u => u.AppUserId == userId).FirstOrDefaultAsync();
    }
}