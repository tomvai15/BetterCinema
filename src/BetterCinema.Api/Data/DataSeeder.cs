namespace BetterCinema.Api.Data
{
    public static class DataSeeder
    {
        public static void AddInitialData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

            dbContext.Database.EnsureCreated();

            dbContext.Theaters.Add(new Models.Theater { Name = "Test1"});

            dbContext.SaveChanges();
        }
    }
}
