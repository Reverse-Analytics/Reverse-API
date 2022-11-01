﻿using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.SupplyDetail
{
    public class SupplyDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPriceDiscount { get; set; }

        public int SupplyId { get; set; }
        public SupplyDto Supply { get; set; }

        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
