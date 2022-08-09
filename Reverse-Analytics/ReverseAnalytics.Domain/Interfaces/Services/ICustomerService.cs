﻿using ReverseAnalytics.Domain.DTOs.Customer;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>?> GetAllCustomerAsync(string? searchString);
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto?> CreateCustomerAsync(CustomerForCreateDto customerToCreate);
        Task UpdateCustomerAsync(CustomerForUpdateDto customerToUpdate);
        Task DeleteCustomerAsync(int id);
    }
}
