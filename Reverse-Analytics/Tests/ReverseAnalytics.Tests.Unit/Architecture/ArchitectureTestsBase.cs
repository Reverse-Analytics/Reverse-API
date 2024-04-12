using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Services;
using System.Reflection;

namespace ReverseAnalytics.Tests.Unit.Architecture;

public class ArchitectureTestsBase
{
    protected readonly Assembly DomainAssembly = typeof(BaseEntity).Assembly;
    protected readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected readonly Assembly ServicesAssembly = typeof(ProductCategoryService).Assembly;

    protected ArchitectureTestsBase()
    {
    }

    protected List<Type> GetEntities()
    {
        return DomainAssembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.IsAssignableTo(typeof(BaseEntity)))
            .ToList();
    }

    protected List<Type> GetConfigurations()
    {
        return InfrastructureAssembly.GetTypes()
            .Where(IsEntityTypeConfiguration)
            .ToList();
    }

    protected List<Type> GetServices()
    {
        return ServicesAssembly.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.Equals("ReverseAnalytics.Services"))
            .Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && !t.IsAnsiClass)
            .ToList();
    }

    protected List<Type> GetMappers()
    {
        return DomainAssembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.IsAssignableTo(typeof(Profile)))
            .ToList();
    }

    protected List<Type> GetQueryParameters()
    {
        return DomainAssembly.GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface)
            .Where(t => t.IsAssignableTo(typeof(QueryParametersBase)))
            .ToList();
    }

    private static bool IsEntityTypeConfiguration(Type type)
    {
        var interfaceType = type.GetInterfaces().ToList();
        return interfaceType.Exists(r => r.IsGenericType && r.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
    }
}
