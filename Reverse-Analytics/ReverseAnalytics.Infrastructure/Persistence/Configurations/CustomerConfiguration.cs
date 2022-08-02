﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);
            builder.HasMany(c => c.CustomerAddresses)
                .WithOne(ca => ca.Customer)
                .HasForeignKey(ca => ca.CustomerId);
            builder.HasMany(c => c.CustomerPhones)
                .WithOne(cp => cp.Customer)
                .HasForeignKey(cp => cp.CustomerId);
        }
    }
}
