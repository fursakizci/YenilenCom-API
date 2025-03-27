using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class ImageRepository:GenericRepository<Image>,IImageRepository
{
    public ImageRepository(AppDbContext context) : base(context)
    {
    }
}