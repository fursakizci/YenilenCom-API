using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Common;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Infrastructure.DataAccess;

internal sealed class AppDbContext:IdentityDbContext<AppUser,AppRole,Guid>, IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;
    private readonly IRequestContextService _requestContextService;
    
    public AppDbContext(DbContextOptions<AppDbContext> options,IRequestContextService requestContextService):base(options)
    {
        _requestContextService = requestContextService;
    }
    
    public DbSet<Customer> Customers { get; set; }
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
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<StoreOwner> StoreOwners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().ToTable("Users");
        modelBuilder.Entity<AppRole>().ToTable("Roles");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _requestContextService.GetCurrentUserId();
        
        if (userId == Guid.Empty)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        
        ApplyAuditInformation(userId);
        
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(Guid? appUserId ,CancellationToken cancellationToken = default)
    { 
        if (appUserId != null)
        {
            ApplyAuditInformation(appUserId);   
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInformation(Guid? userId)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt)
                    .CurrentValue = DateTime.UtcNow;
                entry.Property(p => p.CreateUserId)
                    .CurrentValue = userId ?? Guid.Empty;
            }
    
            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteAt)
                        .CurrentValue = DateTime.UtcNow;
                    entry.Property(p => p.DeleteUserId)
                        .CurrentValue = userId;
                }
                else
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTime.UtcNow;
                    entry.Property(p => p.UpdateUserId)
                        .CurrentValue = userId;
                }
            }
    
            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
            }
        }
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.CommitAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
    
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
}