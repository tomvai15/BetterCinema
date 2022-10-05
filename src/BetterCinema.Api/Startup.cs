using BetterCinema.Api.Bootstrap;

namespace BetterCinema.Api
{
    public static class Startup
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorizationServices(config)
                .AddCinemaDbServices(config)
                .AddHandlers(config)
                .AddMappingProfiles();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
