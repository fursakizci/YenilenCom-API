using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class ServiceRepository:GenericRepository<Service>,IServiceRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Service> _dbSet;
    public ServiceRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Service>();
    }

    public async Task<IEnumerable<Service>> GetServicesByCategoryIdAsync(int id)
    {
        var services = await _dbSet.Where(s => s.CategoryId == id).ToListAsync();

        if (services == null)
            return new List<Service>();
        
        return services;
    }
}