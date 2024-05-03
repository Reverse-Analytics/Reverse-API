namespace ReverseAnalytics.Tests.Unit.Architecture;

public class CommonServicesFixture : ArchitectureTestsBase
{
    [Fact]
    public void Services_ShouldBeSealed()
    {
        // Arrange
        var services = GetServices();

        // Act & Assert
        foreach (var service in services)
        {
            service.IsSealed.Should().BeTrue($"Service: {service.Name} should be sealed.");
        }
    }

    [Fact]
    public void Services_ShouldImplementInterface_FromDomain()
    {
        // Arrange
        var services = GetServices();

        // Act & Assert
        foreach (var service in services)
        {
            var interfaces = service.GetInterfaces();
            var result = interfaces.Where(t => t.Namespace != null && t.Namespace.Contains("Domain.Interfaces.Services")).ToList();

            result.Should().NotBeEmpty($"{service.Name} should implement at least one interface defined in Domain");
        }
    }

    [Fact]
    public void Services_ShouldHaveName_EndWithService()
    {
        // Arrange
        var services = GetServices();

        // Act & Assert
        foreach (var service in services)
        {
            service.Name.Should().EndWith("Service");
        }
    }
}
