using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts;
using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using BetterCinema.Api.Providers;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly CinemaDbContext context;
        private readonly ITheatersHandler theatersHandler;
        private readonly IClaimsProvider claimsProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public TheatersController(CinemaDbContext context, ITheatersHandler theatersHandler, IMapper mapper, IClaimsProvider claimsProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.theatersHandler = theatersHandler;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.claimsProvider = claimsProvider;
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
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PutTheater(int theaterId, UpdateTheaterRequest updateTheaterRequest)
        {
            Theater theater = await context.Theaters.Where(t => t.TheaterId == theaterId).FirstAsync();

            if (theater == null)
            { 
                return NotFound(); 
            }  

            theater.Name = updateTheaterRequest.Name;
            theater.Description = updateTheaterRequest.Description;
            theater.Address = updateTheaterRequest.Address;
            theater.IsConfimed = updateTheaterRequest.IsConfimed;

            context.Entry(theater).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = Role.Owner)]
       // [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<ActionResult<Theater>> PostTheater(CreateTheaterRequest createTheaterRequest)
        {
            Theater newTheater = mapper.Map<Theater>(createTheaterRequest);
            newTheater.IsConfimed = false;

            claimsProvider.TryGetUserId(out int userId);

            newTheater.UserId = userId;
            var addedTheater = context.Theaters.Add(newTheater);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTheater", new { theaterId = addedTheater.Entity.TheaterId }, addedTheater.Entity);
        }

        [HttpDelete("{theaterId}")]
        [Authorize(Policy = AuthPolicy.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
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
