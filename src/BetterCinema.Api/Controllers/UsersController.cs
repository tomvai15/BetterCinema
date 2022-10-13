using AutoMapper;
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
        private readonly IMapper mapper;

        public UsersController(IUserAuthHandler userAuthHandler, IMapper mapper)
        {
            this.userAuthHandler = userAuthHandler;
            this.mapper = mapper;
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
