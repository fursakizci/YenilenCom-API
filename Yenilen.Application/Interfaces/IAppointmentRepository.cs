using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IAppointmentRepository:IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetAppointmentsByStaffAndDateAsync(int staffId, DateTime date);
    Task<List<DailyServiceDurationDto>> GetDailyServiceDurationsByStaffIdAsync(int staffId, DateTime startDate,
        DateTime endDate,string timeZoneId);
    
}
