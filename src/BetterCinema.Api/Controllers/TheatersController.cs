using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly CinemaDbContext _context;
        private readonly ITheatersHandler theatersHandler;

        public TheatersController(CinemaDbContext context, ITheatersHandler theatersHandler)
        {
            _context = context;
            this.theatersHandler = theatersHandler;
        }

        [HttpGet]
        public async Task<ActionResult<GetTheatersResponse>> GetTheaters([FromQuery] int limit, [FromQuery]  int offset )
        {
            return await theatersHandler.GetTheaters(limit, offset);
            
        }

        [HttpGet("{theaterId}")]
        public async Task<ActionResult<Theater>> GetTheater(int theaterId)
        {
            var theater = await _context.Theaters.FindAsync(theaterId);

            if (theater == null)
            {
                return NotFound();
            }

            return theater;
        }

        [HttpPut("{theaterId}")]
        public async Task<IActionResult> PutTheater(int theaterId, Theater theater)
        {
            if (theaterId != theater.TheaterId)
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
                if (!TheaterExists(theaterId))
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
        public async Task<IActionResult> DeleteTheater(int theaterId)
        {
            var theater = await _context.Theaters.FindAsync(theaterId);
            if (theater == null)
            {
                return NotFound();
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TheaterExists(int theaterId)
        {
            return _context.Theaters.Any(e => e.TheaterId == theaterId);
        }
    }
}
