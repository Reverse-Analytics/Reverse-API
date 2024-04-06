using FluentAssertions;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Tests.Unit.Architecture;

public class QueryParametersFixture : DomainBaseFixture
{
    [Fact]
    public void QueryParameters_ShouldEnd_WithQueryParameters()
    {
        // Arrange
        var queryParameters = GetQueryParameters();

        // Act & Assert
        foreach (var type in queryParameters)
        {
            type.Name.Should().EndWith("QueryParameters");
        }
    }

    [Fact]
    public void QueryParameters_ShouldBe_InQueryParametersNamespace()
    {
        // Arrange
        var types = typeof(QueryParametersBase).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(QueryParametersBase).IsAssignableFrom(x))
            .ToList();

        // Act & Assert
        foreach (var type in types)
        {
            type.Namespace.Should().Contain("QueryParameters", $"{type.Name} located in {type.Namespace} instead of QueryParameters namespace.");
        }
    }

    [Fact]
    public void QueryParametersNamespace_ShouldContain_OnlyQueryParameters()
    {
        // Arrange
        var types = typeof(QueryParametersBase).Assembly
            .GetTypes()
            .Where(x => x.Namespace != null && x.Namespace.Contains("Domain.QueryParameters"))
            .ToList();

        // Act & Assert
        foreach (var type in types)
        {
            type.Should().BeAssignableTo(typeof(QueryParametersBase));
        }
    }
}
