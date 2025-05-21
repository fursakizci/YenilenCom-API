using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StoreOwnerRepository : GenericRepository<StoreOwner, AppDbContext>, IStoreOwnerRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<StoreOwner> _dbSet;
    
    public StoreOwnerRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<StoreOwner>();
    }
    
    public async Task<bool> StoreOwnerExistsByPhoneNumberAsync(string phoneNumber = "", string email = "")
    {
        return await _dbSet.AnyAsync(i => i.Email == email || i.PhoneNumber == phoneNumber);
    }

    public async Task<StoreOwner?> GetStoreIdByStoreOwnerIdAsync(Guid? appUserId, CancellationToken cancellationToken)
    {
        var storeOwner = await _dbSet.Include(s=>s.Store).FirstOrDefaultAsync(so => so.AppUserId == appUserId, cancellationToken);
        return storeOwner;
    }
}