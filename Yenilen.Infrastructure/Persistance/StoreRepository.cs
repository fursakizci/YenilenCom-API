using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StoreRepository:GenericRepository<Store, AppDbContext>,IStoreRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Store> _dbSet;
    
    public StoreRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Store>();
    }

    public async Task<string> GetStoreFullAddressById(int id)
    {
        var store = await _dbSet.Include(
                s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null || store.Address == null)
            return string.Empty;

        return store.Address.FullAddress;
    }

    public async Task<Store> GetStoreWithDetailsAsync(int id)
    {
        var store = await _dbSet.Where(s => s.Id == id)
            .Include(s => s.Address)
            .Include(s => s.Reviews)
            .Include(s => s.Categories)
            .ThenInclude(c => c.Services)
            .Include(s => s.Images)
            .Include(s => s.WorkingHours)
            .FirstOrDefaultAsync();

        return store ;
    }

    public async Task<Store> GetStoreWithWorkingTimesAsync(int id)
    {
        var store = await _dbSet.Where(s => s.Id == id)
            .Include(s => s.WorkingHours)
            .FirstOrDefaultAsync();

        return store;
    }

    public async Task<Store?> GetStoreByUserIdAsync(Guid? appUserId)
    {
        return await _dbSet.Include(s => s.StoreOwner).FirstOrDefaultAsync(s => s.StoreOwner.AppUserId == appUserId);
    }

    public async Task<Store?> GetStoreWithCategoriesByUserId(Guid? appUserId, CancellationToken cancellationToken)
    {
        var store = await _dbSet.Include(s => s.Categories)
            .FirstOrDefaultAsync(s => s.StoreOwner.AppUserId == appUserId, cancellationToken);

        return store;
    }
}