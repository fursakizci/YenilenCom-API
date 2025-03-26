using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class AddressRepository:GenericRepository<Address>,IAddressRepository
{
    public AddressRepository(AppDbContext context) : base(context)
    {
    }
}