using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IAppointmentRepository:IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetAppointmentsByStaffAndDateAsync(int staffId, DateTime date);
}
