using Microsoft.EntityFrameworkCore;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.DataAccess;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
}