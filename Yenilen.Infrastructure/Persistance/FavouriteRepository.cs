using Microsoft.EntityFrameworkCore;
using Yenilen.Application.DTOs;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class FavouriteRepository : GenericRepository<Favourite,AppDbContext>, IFavouriteRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Favourite> _dbSet;
    
    public FavouriteRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Favourite>();
    }

    public Task<bool> ExistsAsync(int customerId, int storeId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(f => f.CustomerId == customerId && f.StoreId == storeId, cancellationToken);
    }
}