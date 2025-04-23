using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class StaffWorkingHourConfiguration : IEntityTypeConfiguration<StaffWorkingHour>
{
    public void Configure(EntityTypeBuilder<StaffWorkingHour> builder)
    {
        builder.HasOne(w => w.Staff)
            .WithMany(s => s.WorkingHours)
            .HasForeignKey(w => w.StaffId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}