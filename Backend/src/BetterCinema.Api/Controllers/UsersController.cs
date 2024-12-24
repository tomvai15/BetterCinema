using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Handlers;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IUserAuthHandler userAuthHandler, IMapper mapper, CinemaDbContext cinemaDbContext)
        : ControllerBase
    {
        private readonly IMapper mapper = mapper;

        [HttpGet]
        [Authorize(Policy = AuthPolicy.Admin)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetUsers()
        {
            return cinemaDbContext.Users.ToList();
        }

        [HttpDelete("{userId}")]
        [Authorize(Policy = AuthPolicy.Admin)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> DeleteUser(int userId)
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
            var isUserAdded = await userAuthHandler.TryCreateUser(createUserRequest);

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
                var loginResponse = await userAuthHandler.LoginUser(loginRequest);

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
