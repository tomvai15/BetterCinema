using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<MovieEntity>> GetMovies(int theaterId);
        Task<MovieEntity> GetMovie(int theaterId, int movieId);
        Task<MovieEntity> UpdateMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest);
        Task<MovieEntity> CreateMovie(int theaterId, CreateMovieRequest createMovieRequest);

        Task<bool> DeleteMovie(int theaterId, int movieId);
    }
    public class MoviesRepository(ITheatersHandler theatersHandler, CinemaDbContext context, IMapper mapper)
        : IMoviesRepository
    {
        private readonly ITheatersHandler theatersHandler = theatersHandler;

        public async Task<IEnumerable<MovieEntity>> GetMovies(int theaterId)
        {
            if (!context.Theaters.Any(t => t.Id == theaterId))
            {
                return null;
            }

            var theaterEntity = await context.Theaters.Where(t => t.Id == theaterId)
                .Include(t => t.Movies)
                .ThenInclude(m => m.Sessions)
                .FirstAsync();

            return theaterEntity.Movies.ToList();
        }
        public async Task<MovieEntity> GetMovie(int theaterId, int movieId)
        {
            if (!context.Theaters.Any(t => t.Id == theaterId))
            {
                return null;
            }

            if (!context.Movies.Any(m => m.Id == movieId))
            {
                return null;
            }

            var movie = await context.Movies.Where(m => m.Id == movieId)
                .Include(m => m.Sessions)
                .FirstAsync();

            if (movie.TheaterId != theaterId)
            {
                return null;
            }

            return movie;
        }

        public async Task<MovieEntity> CreateMovie(int theaterId, CreateMovieRequest createMovieRequest)
        {
            var theaterEntity = await context.Theaters.FindAsync(theaterId);

            if (theaterEntity == null)
            {
                return null;
            }

            var newMovieEntity = mapper.Map<MovieEntity>(createMovieRequest);
            newMovieEntity.TheaterId = theaterId;
            var movie = context.Movies.Add(newMovieEntity);
            await context.SaveChangesAsync();
            return movie.Entity;
        }

        public async Task<MovieEntity> UpdateMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest)
        {
            var movieEntity = await GetMovie(theaterId, movieId);
            if (movieEntity == null)
            {
                return null;
            }

            movieEntity.Title = updateMovieRequest.Title;
            movieEntity.Description = updateMovieRequest.Description;
            movieEntity.Genre = updateMovieRequest.Genre;
            movieEntity.Director = updateMovieRequest.Director;
            movieEntity.ReleaseDate = updateMovieRequest.ReleaseDate;

            context.Entry(movieEntity).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return movieEntity;
        }

        public async Task<bool> DeleteMovie(int theaterId, int movieId)
        {
            var movie = await GetMovie(theaterId, movieId);
            if (movie == null)
            {
                return false;
            }

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
