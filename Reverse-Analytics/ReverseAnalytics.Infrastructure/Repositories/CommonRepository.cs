using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class CommonRepository(ApplicationDbContext context) : ICommonRepository
{
    private ICustomerRepository _customer = new CustomerRepository(context);
    public ICustomerRepository Customer => _customer ??= new CustomerRepository(context);

    private IProductCategoryRepository _productCategory = new ProductCategoryRepository(context);
    public IProductCategoryRepository ProductCategory => _productCategory ??= new ProductCategoryRepository(context);

    private IProductRepository _product = new ProductRepository(context);
    public IProductRepository Product => _product ??= new ProductRepository(context);

    private ISaleItemRepository _saleItem = new SaleItemRepository(context);
    public ISaleItemRepository SaleItem => _saleItem ??= new SaleItemRepository(context);

    private ISaleRepository _sale = new SaleRepository(context);
    public ISaleRepository Sale => _sale ??= new SaleRepository(context);

    private ISupplierRepository _supplier = new SupplierRepository(context);
    public ISupplierRepository Supplier => _supplier ??= new SupplierRepository(context);

    private ISupplyItemRepository _supplyItem = new SupplyItemRepository(context);
    public ISupplyItemRepository SupplyItem => _supplyItem ??= new SupplyItemRepository(context);

    private ISupplyRepository _supply = new SupplyRepository(context);
    public ISupplyRepository Supply => _supply ??= new SupplyRepository(context);

    private ITransactionRepository _transaction = new TransactionRepository(context);
    public ITransactionRepository Transaction => _transaction ??= new TransactionRepository(context);

    public async Task<int> SaveChangesAsync()
        => await context.SaveChangesAsync();
}
