﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ReverseAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public ICollection<Supply> Supplies { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
