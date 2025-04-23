using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yenilen.Domain.Entities;

namespace Yenilen.Infrastructure.Configuration;

internal sealed class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(c => c.Store)
            .WithMany(s =>s.Categories)
            .HasForeignKey(c => c.StoreId);

        builder.HasMany(c => c.Services)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId);


    }
}