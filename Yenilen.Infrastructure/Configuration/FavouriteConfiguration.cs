using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class FavouriteConfiguration: IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasOne(f => f.Customer)
            .WithMany(f =>f.Favourites)
            .HasForeignKey(f => f.CustomerId);

        builder.HasOne(f => f.Store)
            .WithOne()
            .HasForeignKey<Favourite>(f => f.StoreId);

    }
}