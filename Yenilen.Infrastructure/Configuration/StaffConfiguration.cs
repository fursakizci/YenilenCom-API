using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasOne(so => so.AppUser)
            .WithOne()
            .HasForeignKey<Staff>(so => so.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Email)
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(s => s.IsAvailable)
            .IsRequired();

        builder.Property(s => s.Bio)
            .HasMaxLength(1000);

        builder.HasOne(s => s.Image)
            .WithMany()
            .HasForeignKey(s => s.ImageId)
            .IsRequired(false);

        builder.HasOne(s => s.Store)
            .WithMany()
            .HasForeignKey(s => s.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.WorkingHours)
            .WithOne()
            .HasForeignKey("StaffId");

        builder.HasMany(s => s.Appointments)
            .WithOne(a => a.Staff)
            .HasForeignKey(a => a.StaffId);
    }
}