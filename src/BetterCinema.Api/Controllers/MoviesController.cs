using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesHandler moviesHandler;
        private readonly IMapper mapper;

        public MoviesController (IMoviesHandler moviesHandler, IMapper mapper)
        {
            this.moviesHandler = moviesHandler;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetMoviesResponse>> GetMovies(int theaterId)
        {
            IEnumerable<Movie> movies = await moviesHandler.GetMovies(theaterId);

            if (movies == null)
            {
                return NotFound();
            }

            IEnumerable<GetMovieResponse> getMovies = mapper.Map<IEnumerable<GetMovieResponse>>(movies);
            return new GetMoviesResponse { Movies = getMovies };
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<GetMovieResponse>> GetMovie(int theaterId, int movieId)
        {
            Movie movie = await moviesHandler.GetMovie(theaterId, movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return mapper.Map<GetMovieResponse>(movie);
        }

        [HttpPatch("{movieId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PatchMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest)
        {
            try
            {
                Movie movie = await moviesHandler.UpdateMovie(theaterId, movieId, updateMovieRequest);
                if (movie == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = Role.Owner)]
        public async Task<ActionResult<Movie>> PostMovie(int theaterId, CreateMovieRequest createMovieRequest)
        {
            Movie movie = await moviesHandler.CreateMovie(theaterId, createMovieRequest);
            if (movie == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetMovie", new { theaterId = theaterId, movieId = movie.MovieId }, movie);
        }

        [HttpDelete("{movieId}")]
        //[Authorize(Roles = Role.Owner)]
        //[Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> DeleteMovie(int theaterId, int movieId)
        {
            try
            {
                bool isMovieDeleted = await moviesHandler.DeleteMovie(theaterId, movieId);
                if (!isMovieDeleted)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }
    }
}
