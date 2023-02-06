﻿using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ISupplyService
    {
        public Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync();
        public Task<IEnumerable<SupplyDto>> GetAllSuppliesBySupplierIdAsync(int supplierId);
        public Task<SupplyDto> GetSupplyByIdAsync(int supplyId);
        public Task<SupplyDto> CreateSupplyAsync(SupplyForCreateDto supplyToCreate);
        public Task UpdateSupplyAsync(SupplyForUpdateDto supplyToUpdate);
        public Task DeleteSupplyAsync(int supplyId);
    }
}
