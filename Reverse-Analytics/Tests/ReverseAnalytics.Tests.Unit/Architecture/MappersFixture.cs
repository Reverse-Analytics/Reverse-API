namespace ReverseAnalytics.Tests.Unit.Architecture;

public class MappersFixture : ArchitectureTestsBase
{
    [Fact]
    public void Mappings_ShouldReside_InMappingsNamespace()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Profile))
            .Should()
            .ResideInNamespace(Namespaces.MAPPINGS)
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void MappingsNamespace_ShouldContain_OnlyMappers()
    {
        // Arrange

        // Act
        var result = Types.InNamespace(Namespaces.MAPPINGS)
            .That()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .Inherit(typeof(Profile))
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EachEntity_ShouldHave_Mapping()
    {
        // Arrange
        var mappers = GetMappers();
        var entities = GetEntities();

        // Act & Assert
        mappers.Count.Should().Be(entities.Count, $"Expected mappings to be: {entities.Count}, but found: {mappers.Count}");
    }

    [Fact]
    public void MappingName_ShouldStartWith_EntityName()
    {
        // Arrange
        var mappers = GetMappers();
        var entities = GetEntities();

        // Act & Assert
        foreach (var mapper in mappers)
        {
            entities.Should().Contain(
                entity => mapper.Name.StartsWith(entity.Name),
                $"Mapping: {mapper.Name} should start with Entity's name");
        }
    }

    [Fact]
    public void MappersName_ShouldEndWith_Mappings()
    {
        // Arrange
        var mappers = GetMappers();

        // Act & Assert
        foreach (var mapper in mappers)
        {
            mapper.Name.Should().EndWith("Mappings", $"Mapper: {mapper.Name} should end with 'Mappings'");
        }
    }
}
