using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAuthHandler userAuthHandler;
        private readonly IMapper mapper;
        private readonly CinemaDbContext cinemaDbContext;

        public UsersController(IUserAuthHandler userAuthHandler, IMapper mapper, CinemaDbContext cinemaDbContext)
        {
            this.userAuthHandler = userAuthHandler;
            this.mapper = mapper;
            this.cinemaDbContext = cinemaDbContext;
        }

        [HttpGet]
        [Authorize(Policy = AuthPolicy.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return cinemaDbContext.Users.ToList();
        }

        [HttpDelete("{userId}")]
        [Authorize(Policy = AuthPolicy.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUser(int userId)
        {
            var user = await cinemaDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            cinemaDbContext.Users.Remove(user);
            await cinemaDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            bool isUserAdded = await userAuthHandler.TryCreateUser(createUserRequest);

            if (!isUserAdded)
            {
                return BadRequest(new { message = "Username is already taken" });
            }
            return NoContent();
        }

        [HttpPost("token")]
        public async Task<ActionResult<LoginResponse>> GetCredentials(LoginRequest loginRequest)
        {
            try
            {
                LoginResponse loginResponse = await userAuthHandler.LoginUser(loginRequest);

                if (loginResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(loginResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
