using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class SupplyConfiguration : IEntityTypeConfiguration<Supply>
{
    public void Configure(EntityTypeBuilder<Supply> builder)
    {
        builder.ToTable(nameof(Supply));
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Supplier)
            .WithMany(x => x.Supplies)
            .HasForeignKey(x => x.SupplierId);
        builder.HasMany(s => s.SupplyItems)
            .WithOne(si => si.Supply)
            .HasForeignKey(si => si.SupplyId);

        builder.Property(s => s.Date)
            .IsRequired();
        builder.Property(s => s.Comments)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
        builder.Property(s => s.TotalDue)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(s => s.TotalPaid)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Ignore(s => s.TransactionSource);
        builder.Ignore(s => s.TransactionType);
    }
}
