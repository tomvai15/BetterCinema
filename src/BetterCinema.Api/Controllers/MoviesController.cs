using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CinemaDbContext context;
        private readonly IMapper mapper;

        public MoviesController(CinemaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(int theaterId)
        {
            var theater = await context.Theaters.Where(t => t.TheaterId == theaterId).Include(t => t.Movies).FirstAsync();

            if (theater == null)
            {
                return NotFound();
            }

            return theater.Movies.ToList();
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<Movie>> GetMovie(int movieId)
        {
            var movie = await context.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPut("{movieId}")]
        public async Task<IActionResult> PutMovie(int movieId, Movie movie)
        {
            if (movieId != movie.MovieId)
            {
                return BadRequest();
            }

            context.Entry(movie).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(int theaterId, CreateMovieRequest createMovieRequest)
        {
            Movie newMovie = mapper.Map<Movie>(createMovieRequest);
            newMovie.TheaterId = theaterId;
            var movie  = context.Movies.Add(newMovie);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new {theaterId = theaterId,  movieId = movie.Entity.MovieId }, movie.Entity);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            var movie = await context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int movieId)
        {
            return context.Sessions.Any(e => e.SessionId == movieId);
        }
    }
}
