using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Handlers;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies")]
    [ApiController]
    public class MoviesController(IMoviesRepository moviesRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetMoviesResponse>> GetMovies(int theaterId)
        {
            IEnumerable<MovieEntity> movies = await moviesRepository.GetMovies(theaterId);

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
            var movieEntity = await moviesRepository.GetMovie(theaterId, movieId);

            if (movieEntity == null)
            {
                return NotFound();
            }

            return mapper.Map<GetMovieResponse>(movieEntity);
        }

        [HttpPatch("{movieId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PatchMovie(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest)
        {
            try
            {
                var movieEntity = await moviesRepository.UpdateMovie(theaterId, movieId, updateMovieRequest);
                if (movieEntity == null)
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
        public async Task<ActionResult<MovieEntity>> PostMovie(int theaterId, CreateMovieRequest createMovieRequest)
        {
            var movieEntity = await moviesRepository.CreateMovie(theaterId, createMovieRequest);
            if (movieEntity == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetMovie", new { theaterId = theaterId, movieId = movieEntity.Id }, movieEntity);
        }

        [HttpDelete("{movieId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> DeleteMovie(int theaterId, int movieId)
        {
            try
            {
                var isMovieDeleted = await moviesRepository.DeleteMovie(theaterId, movieId);
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
