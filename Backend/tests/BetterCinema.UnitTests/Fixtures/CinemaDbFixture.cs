using BetterCinema.Infrastructure.Data;
using BetterCinema.UnitTests.Fakes;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.UnitTests.Fixtures
{
    public class CinemaDbFixture
    {
        private readonly DbContextOptionsBuilder<CinemaDbContext> contextOptionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();

        public DbContextOptions<CinemaDbContext> Options { get; private set; }
        public CinemaDbFixture()
        {
            contextOptionsBuilder.UseInMemoryDatabase(databaseName: "TestDb");
            Options = contextOptionsBuilder.Options;

            using var context = new CinemaDbContext(Options);
            context.Users.AddRange(SampleData.GetUsers());
            context.SaveChanges();
        }
    }
}
