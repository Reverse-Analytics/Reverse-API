﻿using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities
{
    public class Sale : BaseAuditableEntity
    {
        public string Receipt { get; set; }
        public string? Comment { get; set; }
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
        public double DiscountPercentage { get; set; }
        public decimal Discount { get; set; }
        public DateTime SaleDate { get; set; }
        public SaleType SaleType { get; set; }
        public TransactionStatus Status { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleDetail> OrderDetails { get; set; }

        public Sale()
        {
            OrderDetails = new List<SaleDetail>();
        }
    }
}
