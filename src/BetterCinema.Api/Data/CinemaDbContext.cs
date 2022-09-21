using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
           : base(options)
        {
        }

        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
