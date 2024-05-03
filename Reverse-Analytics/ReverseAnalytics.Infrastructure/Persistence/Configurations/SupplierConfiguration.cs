using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable(nameof(Supplier));
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Supplies)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);

            builder.Property(s => s.FirstName)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired();
            builder.Property(s => s.LastName)
                .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
                .IsRequired(false);
            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(s => s.Company)
                .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
                .IsRequired(false);
            builder.Property(s => s.Balance)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
