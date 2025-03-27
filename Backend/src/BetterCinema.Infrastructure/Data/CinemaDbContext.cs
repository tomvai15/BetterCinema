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
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<PermissionEntity> Permissions { get; set; }
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; }
        
        public virtual DbSet<FileMetadataEntity> FileMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TheaterEntity>()
                .HasMany(c => c.Movies)
                .WithOne(e => e.Theater);

            modelBuilder.Entity<UserEntity>(user =>
                {
                    user.HasMany(u => u.Theaters)
                        .WithOne(t => t.User);

                    user.HasMany(c => c.UserRoles)
                        .WithOne(e => e.User);
                }
            );

            modelBuilder.Entity<RoleEntity>(role =>
            {
                role.HasMany(x => x.Permissions)
                    .WithOne(t => t.Role);

                role.HasMany(x => x.UserRoles)
                    .WithOne(t => t.Role);
            });
        }
    }
}