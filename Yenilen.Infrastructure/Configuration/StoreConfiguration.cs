using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.Property(s => s.StoreName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.ManagerName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.ManagerPhone)
            .HasMaxLength(20);

        builder.Property(s => s.MobileNumber)
            .HasMaxLength(20);

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(s => s.About)
            .HasMaxLength(1000);
        
        builder.HasOne(s => s.Address)
            .WithOne()
            .HasForeignKey<Store>(s => s.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Categories)
            .WithOne(c => c.Store)
            .HasForeignKey(c => c.StoreId);

        // builder.HasMany(s => s.Services)
        //     .WithOne()
        //     .HasForeignKey("StoreId");

        builder.HasMany(s => s.StaffMembers)
            .WithOne(s => s.Store)
            .HasForeignKey(s => s.StoreId);

        builder.HasMany(s => s.WorkingHours)
            .WithOne()
            .HasForeignKey("StoreId");

        builder.HasMany(s => s.Images)
            .WithOne(i => i.Store)
            .HasForeignKey(i => i.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}