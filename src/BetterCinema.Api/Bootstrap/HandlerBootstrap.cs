using BetterCinema.Api.Handlers;

namespace BetterCinema.Api.Bootstrap
{
    public static class HandlerBootstrap
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITheatersHandler, TheatersHandler>();
            services.AddTransient<IUserAuthHandler, UserAuthHandler>();
            services.AddTransient<IMoviesHandler, MoviesHandler>();
            services.AddTransient<ISessionsHandler, SessionsHandler>();
            return services;
        }
    }
}
