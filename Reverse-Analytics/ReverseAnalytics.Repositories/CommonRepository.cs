﻿using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IProductCategoryRepository _productCategory;
        public IProductCategoryRepository ProductCategory => _productCategory ??
            new ProductCategoryRepository(_context);

        private readonly IProductRepository _product;
        public IProductRepository Product => _product ??
            new ProductRepository(_context);

        private readonly ICustomerRepository _customer;
        public ICustomerRepository Customer => _customer ??
            new CustomerRepository(_context);

        private readonly ISaleRepository _sale;
        public ISaleRepository Sale => _sale ??
            new SaleRepository(_context);

        private readonly ISaleDetailRepository _saleDetail;
        public ISaleDetailRepository SaleDetail => _saleDetail ??
            new SaleDetailRepository(_context);

        private readonly ISupplierRepository _supplier;
        public ISupplierRepository Supplier => _supplier ??
            new SupplierRepository(_context);

        private readonly ISupplyRepository _supply;
        public ISupplyRepository Supply => _supply ??
            new SupplyRepository(_context);

        private readonly ISupplyDetailRepository _supplyDetail;
        public ISupplyDetailRepository SupplyDetail => _supplyDetail ??
            new SupplyDetailRepository(_context);

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _productCategory = new ProductCategoryRepository(context);
            _product = new ProductRepository(context);
            _customer = new CustomerRepository(context);
            _sale = new SaleRepository(context);
            _saleDetail = new SaleDetailRepository(context);
            _supplier = new SupplierRepository(context);
            _supply = new SupplyRepository(context);
            _supplyDetail = new SupplyDetailRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int savedChanges = await _context.SaveChangesAsync();

            return savedChanges;
        }
    }
}
