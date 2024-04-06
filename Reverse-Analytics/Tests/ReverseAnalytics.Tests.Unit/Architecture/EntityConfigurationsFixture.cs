using FluentAssertions;

namespace ReverseAnalytics.Tests.Unit.Architecture;

public class EntityConfigurationsFixture : DomainBaseFixture
{
    [Fact]
    public void Entities_ShouldHave_Configurations()
    {
        // Arrange
        var entities = GetEntities();
        var configurations = GetConfigurations();

        // Assert & Assert
        entities.Count.Should().Be(configurations.Count);
    }

    [Fact]
    public void ConfigurationNames_ShouldStart_WithEntityNames()
    {
        // Arrange
        var entities = GetEntities();
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var entity in entities)
        {
            configurations.Should().Contain(configuration => configuration.Name.StartsWith(entity.Name));
        }
    }

    [Fact]
    public void ConfigurationNames_ShouldEnd_WithConfiguration()
    {
        // Arrange
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var configuration in configurations)
        {
            configuration.Name.Should().EndWith("Configuration");
        }
    }
}
