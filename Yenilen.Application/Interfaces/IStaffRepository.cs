using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStaffRepository:IGenericRepository<Staff>
{
    Task<IEnumerable<Staff>> GetStaffMembersByStoreId(int id);

    Task<IQueryable<Staff>> GetAppointmentsByStaffAndDateAsync(int id, DateTime date);
}