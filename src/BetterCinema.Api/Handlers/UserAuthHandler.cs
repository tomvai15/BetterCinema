using BetterCinema.Api.Contracts;
using BetterCinema.Api.Models;
using BetterCinema.Api.TokenGeneration;

namespace BetterCinema.Api.Handlers
{
    public interface IUserAuthHandler
    {
        LoginResponse LoginUser(LoginRequest loginRequest);
    }
    public class UserAuthHandler : IUserAuthHandler
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public UserAuthHandler(IJwtTokenGenerator jwtTokenGenerator)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public LoginResponse LoginUser(LoginRequest loginRequest)
        {

            User user = new User
            {
                Role = "Admin"
            };
            string token = jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Token = $"Bearer {token}"
            };
        }
    }
}
