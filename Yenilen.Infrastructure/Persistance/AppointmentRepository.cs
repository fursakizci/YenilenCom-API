using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class AppointmentRepository:GenericRepository<Appointment,AppDbContext>,IAppointmentRepository
{ 
    private readonly AppDbContext _context;
    private readonly DbSet<Appointment> _dbSet;
    public AppointmentRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Appointment>();
    }

    public async Task<List<Appointment>> GetAppointmentsByStaffAndDateAsync(int staffId, DateTime date)
    {
        return await _dbSet.Where(a => a.StaffId == staffId && a.StartTime.Date == date.Date).ToListAsync();
    }
}