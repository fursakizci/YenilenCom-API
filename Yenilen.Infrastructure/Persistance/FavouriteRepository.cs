using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class FavouriteRepository:GenericRepository<Favourite>,IFavouriteRepository
{
    public FavouriteRepository(AppDbContext context) : base(context)
    {
    }
}