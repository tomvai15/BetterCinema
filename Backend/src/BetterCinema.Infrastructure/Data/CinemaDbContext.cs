using BetterCinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Infrastructure.Data
{
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
    {
        public virtual DbSet<TheaterEntity> Theaters { get; set; }
        public virtual DbSet<MovieEntity> Movies { get; set; }
        public virtual DbSet<SessionEntity> Sessions { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TheaterEntity>()
                .HasMany(c => c.Movies)
                .WithOne(e => e.TheaterEntity);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Theaters)
                .WithOne(t => t.UserEntity);
        }
    }
}
