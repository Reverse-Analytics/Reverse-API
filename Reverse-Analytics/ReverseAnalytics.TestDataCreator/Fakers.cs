using Bogus;
using Bogus.Extensions;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.TestDataCreator;

public class Fakers
{
    public Faker<Customer> Customer()
        => new Faker<Customer>()
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+998 ## ###-##-##"))
            .RuleFor(x => x.Company, f => f.Person.Company.Name)
            .RuleFor(x => x.Address, f => f.Person.Address.Street)
            .RuleFor(x => x.Balance, f => f.Random.Decimal2(0, 1_000_000))
            .RuleFor(x => x.Discount, f => f.Random.Double(0, 99));

    public Faker<ProductCategory> ProductCategory()
        => new Faker<ProductCategory>()
            .RuleFor(x => x.Name, f => f.Commerce.Department())
            .RuleFor(x => x.Description, f => f.Lorem.Sentence());

    public Faker<Product> Product(int[] categoryIds)
        => new Faker<Product>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.SupplyPrice, f => f.Random.Decimal2(10_000, 1_000_00))
            .RuleFor(x => x.SalePrice, (f, x) => f.Random.Decimal2(x.SupplyPrice, 1_500_000))
            .RuleFor(x => x.Volume, f => f.Random.Double(10, 100))
            .RuleFor(x => x.Weight, f => f.Random.Double(100, 1000))
            .RuleFor(x => x.UnitOfMeasurement, f => f.Random.Enum<UnitOfMeasurement>())
            .RuleFor(x => x.CategoryId, f => f.Random.ArrayElement(categoryIds));

    public Faker<Sale> Sale(int[] customerIds)
        => new Faker<Sale>()
            .RuleFor(x => x.Date, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddMonths(6)))
            .RuleFor(x => x.Comments, f => f.Lorem.Sentence())
            .RuleFor(x => x.TotalDue, f => f.Random.Decimal2(10_000, 5_000_000))
            .RuleFor(x => x.TotalPaid, f => f.Random.Decimal2(5_000, 6_000_000))
            .RuleFor(x => x.TotalDiscount, (_, x) => x.TotalDue - x.TotalPaid)
            .RuleFor(x => x.SaleType, f => f.Random.Enum<SaleType>())
            .RuleFor(x => x.Status, f => f.Random.Enum<SaleStatus>())
            .RuleFor(x => x.PaymentType, f => f.Random.Enum<PaymentType>())
            .RuleFor(x => x.Currency, f => f.Random.Enum<CurrencyType>())
            .RuleFor(x => x.CustomerId, f => f.Random.ArrayElement(customerIds));

    public Faker<SaleItem> SaleItems(int[] saleIds, int[] productIds)
        => new Faker<SaleItem>()
            .RuleFor(x => x.SaleId, f => f.Random.ArrayElement(saleIds))
            .RuleFor(x => x.ProductId, f => f.Random.ArrayElement(productIds))
            .RuleFor(x => x.UnitPrice, f => f.Random.Decimal2(10_000, 1_000_000))
            .RuleFor(x => x.Quantity, f => f.Random.Int(1, 100));

    public Faker<Supplier> Supplier()
        => new Faker<Supplier>()
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+998 ## ###-##-##"))
            .RuleFor(x => x.Company, f => f.Person.Company.Name)
            .RuleFor(x => x.Balance, f => f.Random.Decimal2(0, 1_000_000));

    public Faker<Supply> Supply(int[] supplierIds)
        => new Faker<Supply>()
            .RuleFor(x => x.Date, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddMonths(6)))
            .RuleFor(x => x.Comments, f => f.Lorem.Sentence())
            .RuleFor(x => x.TotalDue, f => f.Random.Decimal2(10_000, 5_000_000))
            .RuleFor(x => x.TotalPaid, f => f.Random.Decimal2(5_000, 6_000_000))
            .RuleFor(x => x.PaymentType, f => f.Random.Enum<PaymentType>())
            .RuleFor(x => x.Currency, f => f.Random.Enum<CurrencyType>())
            .RuleFor(x => x.SupplierId, f => f.Random.ArrayElement(supplierIds));

    public Faker<SupplyItem> SupplyItems(int[] supplyIds, int[] productIds)
        => new Faker<SupplyItem>()
            .RuleFor(x => x.SupplyId, f => f.Random.ArrayElement(supplyIds))
            .RuleFor(x => x.ProductId, f => f.Random.ArrayElement(productIds))
            .RuleFor(x => x.UnitPrice, f => f.Random.Decimal2(10_000, 1_000_000))
            .RuleFor(x => x.Quantity, f => f.Random.Int(100));
}
