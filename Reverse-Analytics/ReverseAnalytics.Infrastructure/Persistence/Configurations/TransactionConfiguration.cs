using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();
        builder.Property(x => x.SourceId)
            .IsRequired();
        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(t => t.Type)
            .IsRequired();
        builder.Property(t => t.Source)
            .IsRequired();
    }
}
