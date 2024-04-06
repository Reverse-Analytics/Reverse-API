using FluentAssertions;
using NetArchTest.Rules;

namespace ReverseAnalytics.Tests.Unit.Architecture;
public class LayersFixture : DomainBaseFixture
{
    [Fact]
    public void DomainLayer_ShouldNotHave_AnyDependencies()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAll("Infrastructure", "Services", "Api")
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }
}
