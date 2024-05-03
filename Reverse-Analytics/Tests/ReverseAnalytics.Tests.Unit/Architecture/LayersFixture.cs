namespace ReverseAnalytics.Tests.Unit.Architecture;

public class LayersFixture : ArchitectureTestsBase
{
    [Fact]
    public void DomainLayer_ShouldNotHave_AnyDependencies()
    {
        // Arrange

        // Act
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOnAll("Infrastructure", "Services", "Api")
            .GetResult()
            .IsSuccessful;

        // Assert
        result.Should().BeTrue();
    }
}
