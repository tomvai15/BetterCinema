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
        private readonly CinemaDbContext _context;

        public MoviesController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<Movie>> GetMovie(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);

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

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { theaterId = movie.MovieId }, movie);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int movieId)
        {
            return _context.Sessions.Any(e => e.SessionId == movieId);
        }
    }
}
