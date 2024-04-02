using AutoMapper;
using Moq;
using ReverseAnalytics.Domain.Interfaces.Repositories;

namespace ReverseAnalytics.Tests.Unit.Services;
public abstract class ServiceFixtureBase : FixtureBase
{
    protected readonly Mock<ICommonRepository> _mockRepository;
    protected readonly Mock<IMapper> _mockMapper;

    protected ServiceFixtureBase()
    {
        _mockRepository = new Mock<ICommonRepository>();
        _mockMapper = new Mock<IMapper>();
    }
}
