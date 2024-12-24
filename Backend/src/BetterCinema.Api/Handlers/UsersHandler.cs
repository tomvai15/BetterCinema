using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface IUsersHandler
    {
        Task<UserEntity> GetUserByEmail(string email);
        Task<UserEntity> AddNewUser(UserEntity userEntityToAdd);
    }

    public class UsersHandler(CinemaDbContext context) : IUsersHandler
    {
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            if (!context.Users.Any(u => u.Email==email))
            {
                return null;
            }
            return await context.Users.Where(u => u.Email == email).FirstAsync();
        }

        public async Task<UserEntity> GetUserById(int userId)
        {
            if (!context.Users.Any(u => u.Id == userId))
            {
                return null;
            }
            return await context.Users.Where(u => u.Id == userId).FirstAsync();
        }

        public async Task<UserEntity> AddNewUser(UserEntity userEntityToAdd)
        {
            var userEntity = await GetUserByEmail(userEntityToAdd.Email);

            var userExists = userEntity != null;
            if (userExists)
            {
                return null;
            }

            userEntity = context.Users.Add(userEntityToAdd).Entity;

            await context.SaveChangesAsync();

            return userEntity;
        }
    }
}
