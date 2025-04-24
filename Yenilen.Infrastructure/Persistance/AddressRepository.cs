using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class AddressRepository:GenericRepository<Address,AppDbContext>,IAddressRepository
{
    public AddressRepository(AppDbContext context) : base(context)
    {
    }
}