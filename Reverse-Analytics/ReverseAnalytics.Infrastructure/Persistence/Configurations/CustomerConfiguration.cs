using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Sales)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId);

        builder.Property(c => c.FirstName)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired();
        builder.Property(c => c.LastName)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired(false);
        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(c => c.Address)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
        builder.Property(c => c.Company)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
        builder.Property(c => c.Balance)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(c => c.Discount)
            .HasPrecision(18)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
