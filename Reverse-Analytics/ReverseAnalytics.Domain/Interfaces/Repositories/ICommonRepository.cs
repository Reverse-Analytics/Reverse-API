﻿namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IAddressRepository Address { get; }
        public IDebtRepository Debt { get; }
        public IPhoneRepository Phone { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public ICustomerRepository Customer { get; }
        public ISaleRepository Sale { get; }
        public ISaleDetailRepository SaleDetail { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyRepository Supply { get; }
        public ISupplyDetailRepository SupplyDetail { get; }
        public IInventoryRepository Inventory { get; }
        public IInventoryDetailRepository InventoryDetail { get; }

        public Task<int> SaveChangesAsync();
    }
}
