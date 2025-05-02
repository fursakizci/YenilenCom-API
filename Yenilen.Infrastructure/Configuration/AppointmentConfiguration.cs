using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Store)
            .WithMany()
            .HasForeignKey(a => a.StoreId);

        builder.HasOne(a => a.Staff)
            .WithMany()
            .HasForeignKey(a => a.StaffId);

        builder.HasMany(a => a.Services)
            .WithMany();

        builder.Property(a => a.StartTime)
            .IsRequired();

        builder.Property(a => a.Duration)
            .IsRequired();

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.IsPaid)
            .IsRequired();

        builder.Property(a => a.Note)
            .HasMaxLength(500);
    }
}