using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasOne(r => r.Author)
            .WithMany()
            .HasForeignKey(r => r.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Store)
            .WithMany()
            .HasForeignKey(r => r.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Staff)
            .WithMany()
            .HasForeignKey(r => r.StaffId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(r => r.Rating)
            .IsRequired()
            .HasPrecision(2, 1);

        builder.Property(r => r.IsVisible)
            .IsRequired();

        builder.Property(r => r.Reply)
            .HasMaxLength(1000);
    }
}