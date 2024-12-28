using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Bootstrap
{
    public static class CinemaDbBootstrap
    {
        public static IServiceCollection AddCinemaDbServices(this IServiceCollection services, IConfiguration config)
        {
            const string connectionStringSectionName = "CinemaDbContext";
            services.AddDbContext<CinemaDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString(connectionStringSectionName)));

            return services;
        }
    }
}
