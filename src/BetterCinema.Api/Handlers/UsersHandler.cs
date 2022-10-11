using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface IUsersHandler
    {
        Task<User> GetUserByName(string email);
        Task<User> AddUser(User userToAdd);
    }

    public class UsersHandler : IUsersHandler
    {
        private readonly CinemaDbContext context;

        public UsersHandler(CinemaDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByName(string email)
        {
            if (!context.Users.Any(u => u.Email==email))
            {
                return null;
            }
            return await context.Users.Where(u => u.Email == email).FirstAsync();
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
            User user = await GetUserByName(userToAdd.Email);

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
