using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable(nameof(ProductCategory));

        builder.HasKey(pc => pc.Id);

        builder.HasOne(c => c.Parent)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(c => c.ParentId);
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        builder.Property(pc => pc.Name)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired();
        builder.Property(pc => pc.Description)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
    }
}
