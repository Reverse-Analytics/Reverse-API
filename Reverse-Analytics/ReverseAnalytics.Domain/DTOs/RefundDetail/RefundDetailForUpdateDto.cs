﻿namespace ReverseAnalytics.Domain.DTOs.RefundDetail
{
    public class RefundDetailForUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int RefundId { get; set; }
        public int ProductId { get; set; }
    }
}
