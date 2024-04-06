using FluentAssertions;
using NetArchTest.Rules;
using ReverseAnalytics.Domain.Common;
using System.Reflection;

namespace ReverseAnalytics.Tests.Unit.Architecture;

public class EntitiesFixture : DomainBaseFixture
{
    [Fact]
    public void Entities_ShouldResideInEntitiesNamespace()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(BaseEntity))
            .And()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .ResideInNamespace("ReverseAnalytics.Domain.Entities")
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EntitiesNamespace_ShouldContainOnlyEntities()
    {
        // Arrange

        // Act
        var result = Types.InNamespace("ReverseAnalytics.Domain.Entities")
            .Should()
            .Inherit(typeof(BaseEntity))
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void AllNavigationPropertiesInEntities_ShouldBeVirtual()
    {
        // Arrange
        var properties = GetEntities()
            .SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
            .Where(p => (p.PropertyType.IsClass || p.PropertyType.IsInterface) && p.PropertyType != typeof(string))
            .ToList();

        // Act & Assert
        properties.Should().NotBeEmpty();

        foreach (var property in properties)
        {
            property.GetMethod!.IsVirtual.Should().BeTrue($"{property.DeclaringType}.{property.Name} must be declared as virtual.");
        }
    }
}
