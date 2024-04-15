using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Tests.Api;

public class EndpointsBase : IClassFixture<TestingWebAppFactory>
{
    protected readonly HttpClient _client;
    protected readonly TestingWebAppFactory _factory;
    protected readonly ApplicationDbContext _context;

    public EndpointsBase(TestingWebAppFactory factory)
    {
        factory.ClientOptions.BaseAddress = new Uri("https://localhost/api/");
        factory.ClientOptions.AllowAutoRedirect = false;

        _factory = factory;
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}
