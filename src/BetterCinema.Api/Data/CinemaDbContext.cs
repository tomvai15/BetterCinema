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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theater>()
                .HasMany(c => c.Movies)
                .WithOne(e => e.Theater);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Theaters)
                .WithOne(t => t.User);
        }

        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
