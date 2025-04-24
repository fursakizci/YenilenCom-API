using Microsoft.EntityFrameworkCore;
using Yenilen.Application.DTOs;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class FavouriteRepository : GenericRepository<Favourite,AppDbContext>, IFavouriteRepository
{
    public FavouriteRepository(AppDbContext context) : base(context)
    {
        
    }

}