using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Reverse_Analytics.Api.Configurations;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.Validators.Product;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;
using ReverseAnalytics.Infrastructure.Repositories;
using ReverseAnalytics.Services;

namespace Reverse_Analytics.Api.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditInterceptor>();
        services.AddScoped<TransactionsInterceptor>();
        var provider = configuration.GetValue("Provider", "SqlServer");

        services.AddDbContext<ApplicationDbContext>(
            options => _ = provider switch
            {
                "Sqlite" => options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ReverseAnalytics.Migrations.Sqlite")),

                "SqlServer" => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ReverseAnalytics.Migrations.SqlServer")),

                _ => throw new InvalidOperationException($"Unsupported provider: {provider}.")
            });

#if DEBUG

#else
        

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultIdentityConnection"), builder =>
            builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
#endif
        //services.AddScoped<ICommonRepository, CommonRepository>();
        //services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<ISaleRepository, SaleRepository>();
        //services.AddScoped<ISaleItemRepository, SaleItemRepository>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ISaleItemRepository, SaleItemRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplyRepository, SupplyRepository>();
        services.AddScoped<ISupplyItemRepository, SupplyItemRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICommonRepository, CommonRepository>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ProductForCreateValidator>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataSeedConfiguration>(configuration.GetSection(DataSeedConfiguration.SECTION));

        return services;
    }
}
