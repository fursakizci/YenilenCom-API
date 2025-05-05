using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStaffWorkingHourRepository:IGenericRepository<StaffWorkingHour>
{
    Task<List<StaffWorkingHour>> GetStaffWorkingHoursByStaffId(int id);
}