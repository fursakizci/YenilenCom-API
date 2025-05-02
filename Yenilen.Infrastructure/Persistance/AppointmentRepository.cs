using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class AppointmentRepository:GenericRepository<Appointment,AppDbContext>,IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }
}