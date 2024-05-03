using Microsoft.Extensions.Options;
using Reverse_Analytics.Api.Configurations;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.TestDataCreator;

namespace Reverse_Analytics.Api.Extensions;

public class DatabaseSeeder(ApplicationDbContext context, IOptions<DataSeedConfiguration> options)
{
    private readonly ApplicationDbContext _context = context;
    private readonly DataSeedConfiguration _options = options.Value;
    private readonly Fakers _faker = new();

    public void Seed()
    {
        GenerateProductCategories();
        GenerateProducts();
        GenerateCustomers();
        GenerateSales();
        GenerateSaleItems();
        GenerateSuppliers();
        GenerateSupplies();
        GenerateSupplyItems();
        GenerateTransactions();
    }

    private void GenerateProductCategories()
    {
        if (_context.ProductCategories.Any()) return;

        HashSet<string> categoryNames = [];

        for (int i = 0; i < _options.CategoriesCount; i++)
        {
            int attempts = 0;
            var category = _faker.ProductCategory().Generate();

            // try to generate only unique values
            while (categoryNames.Contains(category.Name) && attempts < 100)
            {
                category = _faker.ProductCategory().Generate();
                attempts++;
            }

            // if unable to generate unique value, don't add to context
            if (categoryNames.Contains(category.Name))
            {
                continue;
            }

            _context.ProductCategories.Add(category);
        }

        _context.SaveChanges();
    }

    private void GenerateProducts()
    {
        if (_context.Products.Any()) return;

        HashSet<string> productNames = [];
        var categories = _context.ProductCategories.Select(x => x.Id).ToArray();

        for (int i = 0; i < _options.ProductsCount; i++)
        {
            int attempts = 0;
            var product = _faker.Product(categories).Generate();

            // try to generate only unique values
            while (productNames.Contains(product.Name) && attempts < 100)
            {
                product = _faker.Product(categories).Generate();
                attempts++;
            }

            // if unable to generate unique value, don't add to context
            if (productNames.Contains(product.Name))
            {
                continue;
            }

            _context.Products.Add(product);
        }

        _context.SaveChanges();
    }

    private void GenerateCustomers()
    {
        if (_context.Customers.Any()) return;

        var customers = _faker.Customer().Generate(_options.CustomersCount);

        _context.Customers.AddRange(customers);
        _context.SaveChanges();
    }

    private void GenerateSales()
    {
        if (_context.Sales.Any()) return;

        var customers = _context.Customers.Select(x => x.Id).ToArray();
        var sales = _faker.Sale(customers).Generate(_options.SalesCount);

        _context.Sales.AddRange(sales);
        _context.SaveChanges();
    }

    private void GenerateSaleItems()
    {
        if (_context.SaleItems.Any()) return;

        var sales = _context.Sales.Select(x => x.Id).ToArray();
        var products = _context.Products.Select(x => x.Id).ToArray();
        var saleItems = _faker.SaleItems(sales, products).Generate(_options.SaleItemsCount);

        _context.SaleItems.AddRange(saleItems);
        _context.SaveChanges();
    }

    private void GenerateSuppliers()
    {
        if (_context.Suppliers.Any()) return;

        var suppliers = _faker.Supplier().Generate(_options.SuppliersCount);

        _context.Suppliers.AddRange(suppliers);
        _context.SaveChanges();
    }

    private void GenerateSupplies()
    {
        if (_context.Supplies.Any()) return;

        var suppliers = _context.Suppliers.Select(x => x.Id).ToArray();
        var suppplies = _faker.Supply(suppliers).Generate(_options.SuppliesCount);

        _context.Supplies.AddRange(suppplies);
        _context.SaveChanges();
    }

    private void GenerateSupplyItems()
    {
        if (_context.SupplyItems.Any()) return;

        var supplies = _context.Supplies.Select(x => x.Id).ToArray();
        var products = _context.Products.Select(x => x.Id).ToArray();
        var supplyItems = _faker.SupplyItems(supplies, products).Generate(_options.SupplyItemsCount);

        _context.SupplyItems.AddRange(supplyItems);
        _context.SaveChanges();
    }

    private void GenerateTransactions()
    {
        if (_context.Transactions.Any()) return;

        var sales = _context.Sales.ToArray();
        var supplies = _context.Supplies.ToArray();

        foreach (var sale in sales)
        {
            var transaction = new Transaction
            {
                Date = sale.Date,
                Amount = sale.GetTransactionAmount(),
                Source = sale.TransactionSource,
                Type = sale.TransactionType,
                SourceId = sale.GetTransactionSourceId()
            };

            _context.Transactions.Add(transaction);
        }

        foreach (var supply in supplies)
        {
            var transaction = new Transaction
            {
                Date = supply.Date,
                Amount = supply.GetTransactionAmount(),
                Source = supply.TransactionSource,
                Type = supply.TransactionType,
                SourceId = supply.GetTransactionSourceId()
            };

            _context.Transactions.Add(transaction);
        }

        _context.SaveChanges();
    }
}