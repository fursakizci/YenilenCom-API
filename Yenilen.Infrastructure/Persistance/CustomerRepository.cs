using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class CustomerRepository : GenericRepository<Customer, AppDbContext>, ICustomerRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Customer> _dbSet;
    private readonly UserManager<AppUser> _userManager;

    public CustomerRepository(AppDbContext context, UserManager<AppUser> userManager) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Customer>();
        _userManager = userManager;
    }

    public async Task<bool> CustomerExistsByPhoneNumberAsync(string phoneNumber, string email = "")
    {
        return await _dbSet.AnyAsync(i => i.Email == email || i.PhoneNumber == phoneNumber);
    }

    public async Task<Customer?> GetByIdAsync(Guid? userId, bool includeRelated = false)
    {
        if (!includeRelated)
        {
            return await _dbSet.FindAsync(userId);
        }
        
        if (userId == Guid.Empty || userId is null)
            return new Customer();
        
        return await _dbSet
            .Include(u => u.Addresses)
            .Include(u => u.AvatarUrl)
            .FirstOrDefaultAsync(u => u.AppUserId == userId);
    }

    public async Task<Customer?> GetFavouriteByIdAsync(int id)
    {
        return await _dbSet.Include(u => u.Favourites)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Customer?> GetCustomerByGuid(Guid? userId)
    {
        return await _dbSet.Where(u => u.AppUserId == userId).FirstOrDefaultAsync();
    }

    public async Task<Customer?> GetCustomerWithAvatarAndAddressesAsync(Guid? userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(u => u.Addresses)
            .Include(u => u.AvatarUrl)
            .FirstOrDefaultAsync(u=>u.AppUserId == userId, cancellationToken);
    }
}