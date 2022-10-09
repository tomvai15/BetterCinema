using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Models;
using BetterCinema.Api.TokenGeneration;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BetterCinema.Api.Handlers
{
    public interface IUserAuthHandler
    {
        Task<LoginResponse> LoginUser(LoginRequest loginRequest);
        Task<bool> TryCreateUser(CreateUserRequest createUserRequest);
    }
    public class UserAuthHandler : IUserAuthHandler
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IUsersHandler usersHandler;

        public UserAuthHandler(IJwtTokenGenerator jwtTokenGenerator, IUsersHandler usersHandler)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.usersHandler = usersHandler;
        }
        public async Task<bool> TryCreateUser(CreateUserRequest createUserRequest)
        {
            User user = new User
            {
                UserName = createUserRequest.UserName,
                HashedPassword = BCryptNet.HashPassword(createUserRequest.Password),
                Role = Role.Owner.ToString()
            };

            user = await usersHandler.AddUser(user);

            if (user == null)
            {
                return false;
            }

            return true;

        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            User user = await usersHandler.GetUserByName(loginRequest.UserName);

            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = BCryptNet.Verify(loginRequest.Password, user.HashedPassword);
            
            if (!isCorrectPassword)
            {
                return null;
            }
            string token = jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                UserName = user.UserName,
                Token = token
            };
        }
    }
}
