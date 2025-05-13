using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(r => r.Token)
            .IsRequired();

        builder.Property(r => r.Expires)
            .IsRequired();

        builder.Property(r => r.IsRevoked)
            .IsRequired();

        builder.HasOne(r => r.AppUser)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}