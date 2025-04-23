using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
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
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);

        // builder.OwnsMany(u => u.Addresses, address =>
        // {
        //     address.WithOwner().HasForeignKey("UserId");
        //
        //     address.Property(a => a.Label).HasMaxLength(100);
        //     address.Property(a => a.FullAddress).HasColumnName("FullAddress").HasMaxLength(300);
        //     address.Property(a => a.District).HasMaxLength(100);
        //     address.Property(a => a.City).HasMaxLength(100);
        //     address.Property(a => a.Region).HasMaxLength(100);
        //     address.Property(a => a.Country).HasMaxLength(100);
        //     address.Property(a => a.CountryCode).HasMaxLength(10);
        //     address.Property(a => a.PostCode).HasMaxLength(20);
        //     address.Property(a => a.Latitude);
        //     address.Property(a => a.Longitude);
        //
        //     address.ToTable("UserAddresses");
        // });
        
        builder.HasMany(u => u.Addresses)
            .WithOne()
            .HasForeignKey("UserId");
    }
}