using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public UsersController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { userUd = user.UserId }, user);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { userUd = user.UserId }, user);
        }
    }
}
