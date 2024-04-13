namespace ReverseAnalytics.Tests.Unit;

public abstract class FixtureBase
{
    protected readonly Fixture _fixture;

    protected FixtureBase()
    {
        _fixture = CreateFixture();
    }

    private static Fixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new NullRecursionBehavior());

        return fixture;
    }
}
