using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface IMoviesHandler
    {
        Task<IEnumerable<Movie>> GetMovies(int theaterId);
        Task<Movie> GetMovie(int theaterId, int movieId);
        Task<Movie> UpdateMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest);
        Task<Movie> CreateMovie(int theaterId, CreateMovieRequest createMovieRequest);

        Task<bool> DeleteMovie(int theaterId, int movieId);
    }
    public class MoviesHandler : IMoviesHandler
    {
        private readonly ITheatersHandler theatersHandler;
        private readonly CinemaDbContext context;
        private readonly IMapper mapper;

        public MoviesHandler (ITheatersHandler theatersHandler, CinemaDbContext context, IMapper mapper)
        {
            this.theatersHandler = theatersHandler;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetMovies(int theaterId)
        {
            if (!context.Theaters.Any(t => t.TheaterId == theaterId))
            {
                return null;
            }

            Theater theater = await context.Theaters.Where(t => t.TheaterId == theaterId)
                .Include(t => t.Movies)
                .ThenInclude(m => m.Sessions)
                .FirstAsync();

            return theater.Movies.ToList();
        }
        public async Task<Movie> GetMovie(int theaterId, int movieId)
        {
            if (!context.Theaters.Any(t => t.TheaterId == theaterId))
            {
                return null;
            }

            if (!context.Movies.Any(m => m.MovieId == movieId))
            {
                return null;
            }



            var movie = await context.Movies.Where(m => m.MovieId == movieId)
                .Include(m => m.Sessions)
                .FirstAsync();

            if (movie.TheaterId != theaterId)
            {
                return null;
            }

            return movie;
        }

        public async Task<Movie> CreateMovie(int theaterId, CreateMovieRequest createMovieRequest)
        {
            Movie newMovie = mapper.Map<Movie>(createMovieRequest);
            newMovie.TheaterId = theaterId;
            var movie = context.Movies.Add(newMovie);
            await context.SaveChangesAsync();
            return movie.Entity;
        }

        public async Task<Movie> UpdateMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest)
        {
            Movie movie = await GetMovie(theaterId, movieId);
            if (movie == null)
            {
                return null;
            }

            movie.Title = updateMovieRequest.Title;

            context.Entry(movie).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return movie;
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
