using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;

namespace Yenilen.Infrastructure.Persistance;

public class GenericRepository<TEntity, TContext>:IGenericRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{

    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(TContext context)
    {
        _context = context;
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

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default,
        bool isTrackingActive = true)
    {
        TEntity entity;
        if (isTrackingActive)
        {
            entity = await _dbSet.Where(expression).FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            entity = await _dbSet.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }

    public  IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }

    public IQueryable<TEntity> GetAllWithTracking()
    {
        return _dbSet.AsQueryable();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);

        if (item == null)
        {
            throw new KeyNotFoundException($"Entity with Id {id} not found.");
        }
        
        _dbSet.Remove(item);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(expression, cancellationToken);
    }
    
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.AsNoTracking().Where(expression).AsQueryable();
    }

    public IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression).AsQueryable();
    }

    public IQueryable<KeyValuePair<bool, int>> CountBy(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(expression, cancellationToken);
    }
}