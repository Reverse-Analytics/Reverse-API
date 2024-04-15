using FluentAssertions;

namespace ReverseAnalytics.Tests.Api.Endpoints;

public class EndpointsTests(TestingWebAppFactory factory) : EndpointsBase(factory)
{
    [Theory]
    [InlineData("categories")]
    public async Task Get_EndpointsShouldReturnSuccessAndCorrectStatusType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
    }
}
