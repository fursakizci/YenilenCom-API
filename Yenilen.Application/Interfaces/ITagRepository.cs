using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface ITagRepository:IGenericRepository<Tag>
{
    Task<IEnumerable<Tag>?> GetTagsByTagIds(List<int>? tagIds, CancellationToken cancellationToken);
    Task<IEnumerable<Tag>?> SearchTagsByNameAsync(string? query, int maxResutls, CancellationToken cancellationToken);
}