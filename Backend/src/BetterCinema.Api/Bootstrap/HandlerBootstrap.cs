using BetterCinema.Api.Handlers;
using BetterCinema.Application.FilesManagement;
using BetterCinema.Application.FilesManagement.Interfaces;
using BetterCinema.Domain.Repositories;
using BetterCinema.Infrastructure.FileUpload;
using BetterCinema.Infrastructure.Repositories;
using IMoviesRepository = BetterCinema.Api.Handlers.IMoviesRepository;

namespace BetterCinema.Api.Bootstrap;

public static class HandlerBootstrap
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITheatersHandler, TheatersHandler>();
        services.AddTransient<IUserAuthHandler, UserAuthHandler>();
        services.AddTransient<IUsersHandler, UsersHandler>();
        services.AddTransient<IMoviesRepository, MoviesRepository>();
        services.AddTransient<ISessionsHandler, SessionsHandler>();
        services.AddTransient<IBlobStorageService, BlobStorageService>();

        services.AddRepositories();
        
        services.AddTransient<IFileUploadService, FileUploadService>();
        services.AddTransient<IFilePersistenceService, FilePersistenceService>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var repositoryInterface = typeof(IGenericRepository);
        var types = typeof(GenericRepository<,>).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(repositoryInterface));

        foreach (var type in types)
        {
            var interfaceTypes = type.GetInterfaces().Where(i => i != repositoryInterface);

            foreach (var interfaceType in interfaceTypes)
            {
                services.AddTransient(interfaceType, type);
            }
        }

        return services;
    }
}