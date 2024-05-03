using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class SupplyItemConfiguration : IEntityTypeConfiguration<SupplyItem>
{
    public void Configure(EntityTypeBuilder<SupplyItem> builder)
    {
        builder.ToTable(nameof(SupplyItem));
        builder.HasKey(si => si.Id);

        builder.HasOne(si => si.Supply)
            .WithMany(s => s.SupplyItems)
            .HasForeignKey(si => si.SupplyId);
        builder.HasOne(si => si.Product)
            .WithMany(p => p.SupplyItems)
            .HasForeignKey(si => si.ProductId);

        builder.Property(si => si.Quantity)
            .IsRequired();
        builder.Property(si => si.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}
