using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace ReverseAnalytics.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly AuditInterceptor _auditInterceptor;
    private readonly TransactionsInterceptor _transactionsInterceptor;

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<SaleItem> SaleItems { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<SupplyItem> SupplyItems { get; set; }
    public virtual DbSet<Supply> Supplies { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                AuditInterceptor auditableInterceptor,
                                TransactionsInterceptor transactionsInterceptor)
        : base(options)
    {
        _auditInterceptor = auditableInterceptor;
        _transactionsInterceptor = transactionsInterceptor;

        // Database.EnsureCreated();
        // Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditInterceptor, _transactionsInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ConfigureDecimalAndDouble(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureDecimalAndDouble(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType
                .ClrType
                .GetProperties()
                .Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(double));

            foreach (var property in properties)
            {
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasPrecision(18, 2);
            }
        }
    }
}
