using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface ITagRepository:IGenericRepository<Tag>
{
    Task<IEnumerable<Tag>?> GetTagsByTagIds(List<int>? tagIds, CancellationToken cancellationToken);
}