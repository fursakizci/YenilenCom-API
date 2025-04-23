using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class FavouriteConfiguration: IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasOne(f => f.User)
            .WithMany(f =>f.Favourites)
            .HasForeignKey(f => f.UserId);

        builder.HasOne(f => f.Store)
            .WithOne()
            .HasForeignKey<Favourite>(f => f.StoreId);


        // builder.HasOne(f => f.Store)
        //     .WithMany(f => f.)
        //     .HasForeignKey(f => f.StoreId);
    }
}