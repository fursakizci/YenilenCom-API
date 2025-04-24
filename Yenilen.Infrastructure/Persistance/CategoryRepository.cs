using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class CategoryRepository:GenericRepository<Category,AppDbContext>,ICategoryRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Category> _dbSet;
    public CategoryRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Category>();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesByStoreId(int id)
    {
        return await _dbSet.Where(c => c.StoreId == id).ToListAsync();
    }
}