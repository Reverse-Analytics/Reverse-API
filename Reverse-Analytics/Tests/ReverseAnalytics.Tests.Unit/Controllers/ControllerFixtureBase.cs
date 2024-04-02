using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ReverseAnalytics.Tests.Unit.Controllers;

public abstract class ControllerFixtureBase : FixtureBase
{
    protected readonly HeaderDictionary _headerDictionary;
    protected readonly ControllerContext _controllerContext;

    protected ControllerFixtureBase()
    {
        _headerDictionary = [];
        _controllerContext = GetControllerContext();
    }

    private ControllerContext GetControllerContext()
    {
        var httpResponseMock = new Mock<HttpResponse>();
        httpResponseMock.SetupGet(h => h.Headers).Returns(_headerDictionary); // Set Headers property to the new HeaderDictionary

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.SetupGet(c => c.Response).Returns(httpResponseMock.Object);

        var controllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

        return controllerContext;
    }
}
