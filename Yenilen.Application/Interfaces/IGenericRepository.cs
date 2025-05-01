using System.Linq.Expressions;

namespace Yenilen.Application.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default, bool isTrackingActive = true);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllWithTracking();
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression);

    IQueryable<KeyValuePair<bool, int>> CountBy(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
}