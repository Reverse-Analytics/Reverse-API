using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Tests.Api.Endpoints;

public class ProductCategoryTests(TestingWebAppFactory factory) : EndpointsBase(factory)
{
    public static IEnumerable<object[]> FilterParameters()
    {
        return
        [
            [new Dictionary<string, string> { { "pageSize", "10" } }, 10],
            [new Dictionary<string, string> { { "pageSize", "25" } }, 25],
            [new Dictionary<string, string> { { "pageSize", "60" } }, 50],
            [new Dictionary<string, string> { { "pageSize", "100" } }, 50]
        ];
    }

    [Theory]
    [MemberData(nameof(FilterParameters))]
    public async Task GetRequest_ShouldCorrectlyPaginate(Dictionary<string, string> filterParameters, int expectedCount)
    {
        // Arrange
        var query = string.Join("&", filterParameters.Select(kv => $"{kv.Key}={kv.Value}"));

        // Act
        var response = await _client.GetFromJsonAsync<List<ProductCategoryDto>>("categories" + $"?{query}");

        // Assert
        response.Should().NotBeEmpty();
        response!.Count.Should().Be(expectedCount);
    }

    [Fact]
    public async Task GetById_ShouldReturnCorrectCategory()
    {
        // Arrange
        var expected = await _context.ProductCategories
            .Include(x => x.Parent)
            .Include(x => x.SubCategories)
            .Include(x => x.Products)
            .FirstOrDefaultAsync();

        // Act
        var actual = await _client.GetFromJsonAsync<ProductCategoryDto>($"categories/{expected!.Id}");

        // Assert
        Assert.NotNull(actual);
        ValidateCategory(expected, actual);

        foreach (var child in expected.SubCategories)
        {
            ValidateCategory(child, actual);
        }
    }

    private static void ValidateCategory(ProductCategory expected, ProductCategoryDto actual)
    {
        actual.Should().NotBeNull();
        actual.Id.Should().Be(expected.Id);
        actual.Name.Should().Be(expected.Name);
        actual.Description.Should().Be(expected.Description);
        actual.ParentId.Should().Be(expected.ParentId);
        actual.ParentName.Should().Be(expected.Parent?.Name);
    }
}
