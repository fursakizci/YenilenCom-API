using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
{

    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public  Task UpdateAsync(TEntity entity)
    {
        //TODO: Implement update logic 
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);

        if (item == null)
        {
            throw new KeyNotFoundException($"Entity with Id {id} not found.");
        }
        
        _dbSet.Remove(item);

        await _context.SaveChangesAsync();
    }
}