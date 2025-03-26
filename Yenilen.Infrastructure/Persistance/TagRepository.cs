using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class TagRepository:GenericRepository<Tag>,ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
}