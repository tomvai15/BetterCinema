using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface IUsersHandler
    {
        Task<User> GetUserByName(string userName);
        Task<User> AddUser(User userToAdd);
    }

    public class UsersHandler : IUsersHandler
    {
        private readonly CinemaDbContext context;

        public UsersHandler(CinemaDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByName(string userName)
        {
            if (!context.Users.Any(u => u.UserName==userName))
            {
                return null;
            }
            return await context.Users.Where(u => u.UserName == userName).FirstAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            if (!context.Users.Any(u => u.UserId == userId))
            {
                return null;
            }
            return await context.Users.Where(u => u.UserId == userId).FirstAsync();
        }

        public async Task<User> AddUser(User userToAdd)
        {
            User user = await GetUserByName(userToAdd.UserName);

            if (user != null)
            {
                return null;
            }

            user = context.Users.Add(userToAdd).Entity;

            await context.SaveChangesAsync();

            return user;
        }
    }
}
