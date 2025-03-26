using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class ServiceRepository:GenericRepository<Service>,IServiceRepository
{
    public ServiceRepository(AppDbContext context) : base(context)
    {
    }
}