using AutoMapper;
using BetterCinema.Api.Mapping;

namespace BetterCinema.Api.Bootstrap
{
    public static class MappingBootstrap
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TheaterModelsMappingProfile());
                cfg.AddProfile(new MovieModelsMappingProfile());
                cfg.AddProfile(new SessionModelsMappingProfile());
                cfg.AddProfile(new UserModelsMappingProfile());
            });

            services.AddSingleton(config.CreateMapper());
            return services;
        }
    }
}
