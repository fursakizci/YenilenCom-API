using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(r => r.AppUser)
            .HasForeignKey(r => r.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}