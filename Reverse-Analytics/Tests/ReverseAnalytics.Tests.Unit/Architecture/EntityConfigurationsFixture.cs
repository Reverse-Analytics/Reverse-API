namespace ReverseAnalytics.Tests.Unit.Architecture;

public class EntityConfigurationsFixture : ArchitectureTestsBase
{
    [Fact]
    public void Configurations_ShouldReside_InConfigurationsNamespace()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .And()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .ResideInNamespace(Namespaces.ENTITY_CONFIGURATIONS)
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ConfigurationsNamespace_ShouldContain_OnlyEntityConfigurations()
    {
        // Arrange

        // Act
        var result = Types.InNamespace(Namespaces.ENTITY_CONFIGURATIONS)
            .That()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EachEntity_ShouldHave_Configuration()
    {
        // Arrange
        var entities = GetEntities();
        var configurations = GetConfigurations();

        // Assert & Assert
        entities.Count.Should().Be(configurations.Count, $"Expected entity configurations to be: {entities.Count}, but found: {configurations.Count}");
    }

    [Fact]
    public void ConfigurationName_ShouldStart_WithEntityName()
    {
        // Arrange
        var entities = GetEntities();
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var configuration in configurations)
        {
            entities.Should().Contain(
                entity => configuration.Name.StartsWith(entity.Name),
                $"Configuration: {configuration.Name} should start with 'Entity' name");
        }
    }

    [Fact]
    public void ConfigurationName_ShouldEnd_WithConfiguration()
    {
        // Arrange
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var configuration in configurations)
        {
            configuration.Name.Should().EndWith("Configuration", $"{configuration.Name} should end with 'Configuration'");
        }
    }
}
