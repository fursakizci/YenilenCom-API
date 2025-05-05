using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StaffWorkingHourRepository: GenericRepository<StaffWorkingHour,AppDbContext>, IStaffWorkingHourRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<StaffWorkingHour> _dbSet;
    public StaffWorkingHourRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<StaffWorkingHour>();
    }

    public async Task<List<StaffWorkingHour>> GetStaffWorkingHoursByStaffId(int id)
    {
        return await _dbSet.Where(s => s.StaffId == id).ToListAsync();
    }
}