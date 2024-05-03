namespace ReverseAnalytics.Tests.Unit.Architecture;

public class QueryParametersFixture : ArchitectureTestsBase
{
    [Fact]
    public void QueryParameters_ShouldReside_InQueryParametersNamespace()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(QueryParametersBase))
            .And()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .ResideInNamespace(Namespaces.QUERY_PARAMETERS)
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void QueryParametersNamespace_ShouldContain_OnlyQueryParameters()
    {
        // Arrange

        // Act
        var result = Types.InNamespace(Namespaces.QUERY_PARAMETERS)
            .That()
            .AreNotAbstract()
            .And()
            .AreNotInterfaces()
            .Should()
            .Inherit(typeof(QueryParametersBase))
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void QueryParameters_ShouldEnd_WithQueryParameters()
    {
        // Arrange
        var queryParameters = GetQueryParameters();

        // Act & Assert
        foreach (var type in queryParameters)
        {
            type.Name.Should().EndWith("QueryParameters", $"{type.Name} should end with 'QueryParameters'.");
        }
    }
}
