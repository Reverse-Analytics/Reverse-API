namespace ReverseAnalytics.Tests.Unit.Architecture;

public class EntitiesFixture : ArchitectureTestsBase
{
    [Fact]
    public void Entities_ShouldReside_InEntitiesNamespace()
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
            .ResideInNamespace(Namespaces.ENTITIES)
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EntitiesNamespace_ShouldContain_OnlyEntities()
    {
        // Arrange

        // Act
        var result = Types.InNamespace(Namespaces.ENTITIES)
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
