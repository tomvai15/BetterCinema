namespace BetterCinema.Api.Data
{
    public static class DataSeeder
    {
        public static void AddInitialData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

            dbContext.Database.EnsureCreated();

            for (int i = 0; i < 10; i++)
            {
                dbContext.Theaters.Add(new Models.Theater
                {
                    Name = $"Borum Cinemas{Guid.NewGuid().ToString().Substring(0,4)}",
                    Location = "Šventų gatvė 4a",
                    Description = "Labai geras kino teatras"
                });
            }

            dbContext.SaveChanges();
        }
    }
}
