using FluentAssertions;

namespace ReverseAnalytics.Tests.Unit.Architecture;

public class MappersFixture : DomainBaseFixture
{
    [Fact]
    public void AllEntities_ShouldHave_Mappings()
    {
        // Arrange
        var mappers = GetMappers();
        var entities = GetEntities();

        // Act & Assert
        mappers.Count.Should().Be(entities.Count);
    }

    [Fact]
    public void MappersName_ShouldStartWith_EntityNames()
    {
        // Arrange
        var mappers = GetMappers();
        var entities = GetEntities();

        // Act & Assert
        foreach (var entity in entities)
        {
            mappers.Should().Contain(mapper => mapper.Name.StartsWith(entity.Name));
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
            mapper.Name.Should().EndWith("Mappings");
        }
    }
}
