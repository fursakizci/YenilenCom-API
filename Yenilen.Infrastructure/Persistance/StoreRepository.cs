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
}