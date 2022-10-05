using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts;
using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly CinemaDbContext context;
        private readonly ITheatersHandler theatersHandler;
        private readonly IMapper mapper;

        public TheatersController(CinemaDbContext context, ITheatersHandler theatersHandler, IMapper mapper)
        {
            this.context = context;
            this.theatersHandler = theatersHandler;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetTheatersResponse>> GetTheaters([FromQuery] int limit, [FromQuery]  int offset )
        {
            return await theatersHandler.GetTheaters(limit, offset);           
        }

        [HttpGet("{theaterId}")]
        public async Task<ActionResult<Theater>> GetTheater(int theaterId)
        {
            var theater = await context.Theaters.FindAsync(theaterId);

            if (theater == null)
            {
                return NotFound();
            }

            return theater;
        }

        [HttpPatch("{theaterId}")]
        public async Task<IActionResult> PutTheater(int theaterId, UpdateTheaterRequest updateTheaterRequest)
        {
            Theater theater = await context.Theaters.Where(t => t.TheaterId == theaterId).FirstAsync();

            if (updateTheaterRequest.Name!=null)
            {
                theater.Name = updateTheaterRequest.Name;
            }

            context.Entry(theater).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
        public async Task<ActionResult<Theater>> PostTheater(CreateTheaterRequest createTheaterRequest)
        {
            Theater newTheater = mapper.Map<Theater>(createTheaterRequest);
            var addedTheater = context.Theaters.Add(newTheater);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTheater", new { theaterId = addedTheater.Entity.TheaterId }, addedTheater.Entity);
        }

        [HttpDelete("{theaterId}")]
        public async Task<IActionResult> DeleteTheater(int theaterId)
        {
            var theater = await context.Theaters.FindAsync(theaterId);
            if (theater == null)
            {
                return NotFound();
            }

            context.Theaters.Remove(theater);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool TheaterExists(int theaterId)
        {
            return context.Theaters.Any(e => e.TheaterId == theaterId);
        }
    }
}
