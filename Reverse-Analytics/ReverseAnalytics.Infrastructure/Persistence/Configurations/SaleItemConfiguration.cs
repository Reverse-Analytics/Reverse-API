using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable(nameof(SaleItem));

        builder.HasKey(si => si.Id);

        builder.HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId);
        builder.HasOne(si => si.Product)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(sd => sd.ProductId);

        builder.Property(si => si.Quantity)
            .IsRequired();
        builder.Property(si => si.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(si => si.Discount)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
