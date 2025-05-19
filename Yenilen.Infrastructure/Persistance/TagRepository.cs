using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class TagRepository:GenericRepository<Tag, AppDbContext>,ITagRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Tag> _dbSet;
    public TagRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Tag>();
    }

    public async Task<IEnumerable<Tag>?> GetTagsByTagIds(List<int>? tagIds, CancellationToken cancellationToken)
    {
        if (tagIds == null || !tagIds.Any())
            return Enumerable.Empty<Tag>();

        return await _dbSet.Where(tag => tagIds.Contains(tag.Id)).ToListAsync();
    }
}