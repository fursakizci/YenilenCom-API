using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class ImageRepository:GenericRepository<Image,AppDbContext>,IImageRepository
{
    public ImageRepository(AppDbContext context) : base(context)
    {
    }
}