using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

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
    }
}