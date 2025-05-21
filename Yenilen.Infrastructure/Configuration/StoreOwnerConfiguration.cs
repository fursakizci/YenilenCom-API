using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

public class StoreOwnerConfiguration : IEntityTypeConfiguration<StoreOwner>
{
    public void Configure(EntityTypeBuilder<StoreOwner> builder)
    {
        builder.HasOne(so => so.AppUser)
            .WithOne()
            .HasForeignKey<StoreOwner>(so => so.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(so => so.Store)
            .WithOne(s => s.StoreOwner)
            .HasForeignKey<Store>(s => s.StoreOwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}