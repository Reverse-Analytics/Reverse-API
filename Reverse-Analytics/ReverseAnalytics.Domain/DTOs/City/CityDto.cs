﻿using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.DTOs.City
{
    public class CityDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
