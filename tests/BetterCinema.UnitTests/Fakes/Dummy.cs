using AutoFixture;

namespace BetterCinema.UnitTests.Fakes
{
    public static class Dummy
    {
        private static Fixture fixture = new Fixture();

        public static T Any<T>() => fixture.Create<T>();
    }
}
