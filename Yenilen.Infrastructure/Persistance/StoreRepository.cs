using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class StoreRepository:GenericRepository<Store>,IStoreRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Store> _dbSet;
    
    public StoreRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Store>();
    }
}