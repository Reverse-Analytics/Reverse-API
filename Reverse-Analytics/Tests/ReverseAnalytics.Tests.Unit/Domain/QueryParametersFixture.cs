using FluentAssertions;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Tests.Unit.Domain;

public class QueryParametersFixture
{
    [Fact]
    public void QueryParameters_ShouldEndWithQueryParametersName()
    {
        var types = typeof(QueryParametersBase).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(QueryParametersBase).IsAssignableFrom(x))
            .ToList();

        foreach (var type in types)
        {
            type.Name.Should().EndWith("QueryParameters");
        }
    }

    [Fact]
    public void QueryParameters_ShouldBeInQueryParametersNamespace()
    {
        var types = typeof(QueryParametersBase).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(QueryParametersBase).IsAssignableFrom(x))
            .ToList();

        foreach (var type in types)
        {
            type.Namespace.Should().Contain("QueryParameters", $"{type.Name} located in {type.Namespace} instead of QueryParameters namespace.");
        }
    }
}
