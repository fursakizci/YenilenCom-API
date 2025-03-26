using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class StaffRepository:GenericRepository<Staff>,IStaffRepository
{
    public StaffRepository(AppDbContext context) : base(context)
    {
    }
}