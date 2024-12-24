using BetterCinema.Api.Bootstrap;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
