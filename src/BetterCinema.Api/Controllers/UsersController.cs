using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAuthHandler userAuthHandler;
        private readonly IUsersHandler usersHandler;

        public UsersController(IUserAuthHandler userAuthHandler, IUsersHandler usersHandler)
        {
            this.userAuthHandler = userAuthHandler;
            this.usersHandler = usersHandler;
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
            LoginResponse loginResponse = await userAuthHandler.LoginUser(loginRequest);

            if (loginResponse == null)
            {
                return Unauthorized();
            }

            return Ok(loginResponse);
        }
    }
}
