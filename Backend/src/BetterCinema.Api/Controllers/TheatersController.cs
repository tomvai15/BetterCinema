using Microsoft.AspNetCore.Mvc;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts;
using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Constants;
using BetterCinema.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController(CinemaDbContext context, ITheatersHandler theatersHandler, IMapper mapper)
        : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<GetTheatersResponse>> GetTheaters([FromQuery] int limit, [FromQuery]  int offset )
        {
            return await theatersHandler.GetTheaters(limit, offset);
        }

        [HttpGet("{theaterId}")]
        public async Task<ActionResult<GetTheaterResponse>> GetTheater(int theaterId)
        {
            var theater = await context.Theaters.FindAsync(theaterId);

            if (theater == null)
            {
                return NotFound();
            }


            return mapper.Map<GetTheaterResponse>(theater);
        }

        [HttpPatch("{theaterId}")]
        //[Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PutTheater(int theaterId, UpdateTheaterRequest updateTheaterRequest)
        {
            var theaterEntity = await context.Theaters.Where(t => t.Id == theaterId).FirstAsync();

            if (theaterEntity == null)
            { 
                return NotFound(); 
            }  

            theaterEntity.Name = updateTheaterRequest.Name ?? theaterEntity.Name;
            theaterEntity.Description = updateTheaterRequest.Description ?? theaterEntity.Description;
            theaterEntity.Address = updateTheaterRequest.Address ?? theaterEntity.Address;
            theaterEntity.ImageUrl = updateTheaterRequest.ImageUrl ?? theaterEntity.ImageUrl;
            theaterEntity.IsConfirmed = updateTheaterRequest.IsConfirmed ?? theaterEntity.IsConfirmed;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        // [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<ActionResult<TheaterEntity>> PostTheater(CreateTheaterRequest createTheaterRequest)
        {
            var newTheaterEntity = mapper.Map<TheaterEntity>(createTheaterRequest);
            newTheaterEntity.IsConfirmed = false;
            var userId = 1;
           // claimsProvider.TryGetUserId(out int userId);

            newTheaterEntity.UserId = userId;
            newTheaterEntity.IsConfirmed = false;
            var addedTheater = context.Theaters.Add(newTheaterEntity);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTheater", new { theaterId = addedTheater.Entity.Id }, addedTheater.Entity);
        }

        [HttpDelete("{theaterId}")]
        [Authorize(Roles = $"{Role.Owner},{Role.Admin}")]
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
    }
}
