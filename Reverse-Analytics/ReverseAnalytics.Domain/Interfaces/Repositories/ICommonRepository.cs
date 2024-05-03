namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ICommonRepository
{
    public ICustomerRepository Customer { get; }
    public IProductCategoryRepository ProductCategory { get; }
    public IProductRepository Product { get; }
    public ISaleItemRepository SaleItem { get; }
    public ISaleRepository Sale { get; }
    public ISupplierRepository Supplier { get; }
    public ISupplyItemRepository SupplyItem { get; }
    public ISupplyRepository Supply { get; }
    public ITransactionRepository Transaction { get; }

    public Task<int> SaveChangesAsync();
}
