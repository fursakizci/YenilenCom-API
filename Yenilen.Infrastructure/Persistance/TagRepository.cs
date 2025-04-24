using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class TagRepository:GenericRepository<Tag, AppDbContext>,ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
}