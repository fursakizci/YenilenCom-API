using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class AppointmentRepository:GenericRepository<Appointment>,IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }
}