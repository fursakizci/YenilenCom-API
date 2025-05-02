using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StaffRepository:GenericRepository<Staff, AppDbContext>,IStaffRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Staff> _dbSet;
    public StaffRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Staff>();
    }

    public async Task<IEnumerable<Staff>> GetStaffMembersByStoreId(int id)
    {
        return await _dbSet.Where(s => s.StoreId == id)
            .Include(i=>i.Image)
            .ToListAsync();
    }

    public Task<IQueryable<Staff>> GetAppointmentsByStaffAndDateAsync(int id, DateTime date)
    {
        throw new NotImplementedException();
    }
}