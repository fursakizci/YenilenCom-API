using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.DataAccess;

internal sealed class AppDbContext:DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Staff> StaffMembers { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}