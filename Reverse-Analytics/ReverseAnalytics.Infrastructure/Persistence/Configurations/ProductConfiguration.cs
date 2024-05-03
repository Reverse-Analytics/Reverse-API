using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Category)
            .WithMany(pc => pc.Products)
            .HasForeignKey(p => p.CategoryId);
        builder.HasMany(p => p.SaleItems)
            .WithOne(si => si.Product)
            .HasForeignKey(si => si.ProductId);

        builder.Property(p => p.Name)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired();
        builder.Property(p => p.Code)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired();
        builder.Property(p => p.Description)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
        builder.Property(p => p.SalePrice)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(p => p.SupplyPrice)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(p => p.Volume)
            .HasPrecision(18, 2)
            .IsRequired(false);
        builder.Property(p => p.Weight)
            .HasPrecision(18, 2)
            .IsRequired(false);
    }
}
