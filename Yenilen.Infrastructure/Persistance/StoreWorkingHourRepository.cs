using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StoreWorkingHourRepository:GenericRepository<StoreWorkingHour,AppDbContext>, IStoreWorkingHourRepository
{
    public StoreWorkingHourRepository(AppDbContext context) : base(context)
    {
    }
}