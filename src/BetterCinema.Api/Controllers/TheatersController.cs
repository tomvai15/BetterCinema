using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public TheatersController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetTheaters()
        {
            return await _context.Theaters.ToListAsync();
        }

        [HttpGet("{theaterId}")]
        public async Task<ActionResult<Theater>> GetTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound();
            }

            return theater;
        }

        [HttpPut("{theaterId}")]
        public async Task<IActionResult> PutTheater(int id, Theater theater)
        {
            if (id != theater.TheaterId)
            {
                return BadRequest();
            }

            _context.Entry(theater).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheaterExists(id))
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
        public async Task<ActionResult<Theater>> PostTheater(Theater theater)
        {
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheater", new { theaterId = theater.TheaterId }, theater);
        }

        [HttpDelete("{theaterId}")]
        public async Task<IActionResult> DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null)
            {
                return NotFound();
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TheaterExists(int id)
        {
            return _context.Theaters.Any(e => e.TheaterId == id);
        }
    }
}
