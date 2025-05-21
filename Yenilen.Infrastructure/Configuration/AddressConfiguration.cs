using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        
        builder.HasOne(a => a.Customer)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // builder.HasOne(a => a.Store)
        //     .WithOne(s => s.Address)
        //     .HasForeignKey<Address>(a => a.StoreId)
        //     .OnDelete(DeleteBehavior.Restrict);
        //
        // builder.Property(a => a.StoreId)
        //     .IsRequired(false);

        builder.Property(a => a.CustomerId)
            .IsRequired(false);
    }
}