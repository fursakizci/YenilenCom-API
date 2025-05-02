using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StaffWorkingHourRepository: GenericRepository<StaffWorkingHour,AppDbContext>, IStaffWorkingHourRepository
{
    public StaffWorkingHourRepository(AppDbContext context) : base(context)
    {
    }
}