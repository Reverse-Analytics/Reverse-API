﻿namespace ReverseAnalytics.Tests.Unit.Architecture;

public class ConstructorsFixture : ArchitectureTestsBase
{
    [Fact]
    public void ConstructorsOfAllControllers_ShouldThrowArgumentNullException_WhenAnyParameterIsNull()
    {
        // Arrange
        var constructors = Types.InAssembly(DomainAssembly)
            .GetTypes()
            .Where(t => t.Namespace is not null && t.Namespace.Contains("Controllers"))
            .Where(t => t.IsAssignableTo(typeof(ControllerBase)))
            .SelectMany(t => t.GetConstructors())
            .Where(c => c.GetParameters().Length > 0)
            .ToList();

        // Act & Assert
        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var nullArgs = new object[parameters.Length];
            var ex = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(nullArgs));
            var innerException = ex.InnerException;

            Assert.NotNull(innerException);
            Assert.IsType<ArgumentNullException>(innerException);

            var paramName = (innerException as ArgumentNullException)?.ParamName;
            Assert.Contains(paramName, parameters.Select(p => p.Name));
        }
    }

    [Fact]
    public void ConstructorsOfAllCoreServices_ShouldThrowArgumentNullException_WhenAnyParameterIsNull()
    {
        // Arrange
        var constructors = typeof(ProductCategoryService).Assembly
            .GetTypes()
            .Where(t => t.Namespace is not null && t.Namespace.Contains("Services"))
            .SelectMany(t => t.GetConstructors())
            .Where(t => t.GetParameters().Length > 0)
            .ToList();

        // Act & Assert
        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var nullArgs = new object[parameters.Length];
            var ex = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(nullArgs));
            var innerException = ex.InnerException;

            innerException.Should().NotBeNull();
            innerException.Should().BeAssignableTo<ArgumentNullException>();

            var paramName = (innerException as ArgumentNullException)?.ParamName;
            parameters.Should().Contain(p => p.Name == paramName);
        }
    }

    [Fact]
    public void ConstructorsOfAllRepositories_ShouldThrowArgumentNullException_WhenAnyParameterIsNull()
    {
        // Arrange
        var constructors = typeof(ProductCategoryRepository).Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(RepositoryBase<>).IsAssignableFrom(t))
            .SelectMany(t => t.GetConstructors())
            .Where(t => t.GetParameters().Length > 0)
            .ToList();

        // Act & Assert
        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var nullArgs = new object[parameters.Length];
            var ex = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(nullArgs));
            var innerException = ex.InnerException;

            innerException.Should().NotBeNull();
            innerException.Should().BeAssignableTo<ArgumentNullException>();

            var paramName = (innerException as ArgumentNullException)?.ParamName;
            parameters.Should().Contain(p => p.Name == paramName);
        }
    }
}
