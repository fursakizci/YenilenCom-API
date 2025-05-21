using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasOne(u => u.AppUser)
            .WithMany()
            .HasForeignKey(u => u.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Email)
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(u => u.Gender)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(u => u.AvatarUrl)
            .WithMany()
            .HasForeignKey("AvatarImageId")
            .IsRequired(false);

        builder.HasMany(u => u.Addresses)
            .WithOne()
            .HasForeignKey("UserId");

        builder.HasMany(u => u.Favourites)
            .WithOne(f => f.Customer)
            .HasForeignKey(f => f.CustomerId);
        
        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Appointments)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}