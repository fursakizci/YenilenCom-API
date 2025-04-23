using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class StoreWorkingHourConfiguration : IEntityTypeConfiguration<StoreWorkingHour>
{
    public void Configure(EntityTypeBuilder<StoreWorkingHour> builder)
    {
        builder.Property(w => w.DayOfWeek)
            .IsRequired();

        builder.Property(w => w.OpeningTime)
            .IsRequired();

        builder.Property(w => w.ClosingTime)
            .IsRequired();

        builder.HasOne(w => w.Store)
            .WithMany(s => s.WorkingHours)
            .HasForeignKey(w => w.StoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}