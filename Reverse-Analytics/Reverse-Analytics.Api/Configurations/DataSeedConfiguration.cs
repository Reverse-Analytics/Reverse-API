namespace Reverse_Analytics.Api.Configurations;

public sealed class DataSeedConfiguration
{
    public const string SECTION = "DataSeed";

    public int CategoriesCount { get; init; }
    public int ProductsCount { get; init; }
    public int CustomersCount { get; init; }
    public int SalesCount { get; init; }
    public int SaleItemsCount { get; set; }
    public int SuppliersCount { get; init; }
    public int SuppliesCount { get; init; }
    public int SupplyItemsCount { get; set; }
}
