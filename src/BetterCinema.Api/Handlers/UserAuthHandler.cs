using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Models;
using BetterCinema.Api.Cryptography;

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
        private readonly IMapper mapper;
        private readonly IHasherAdapter hasherAdapter;

        public UserAuthHandler(IJwtTokenGenerator jwtTokenGenerator, IUsersHandler usersHandler, IMapper mapper, IHasherAdapter hasherAdapter)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.usersHandler = usersHandler;
            this.mapper = mapper;
            this.hasherAdapter = hasherAdapter;
        }
        public async Task<bool> TryCreateUser(CreateUserRequest createUserRequest)
        {
            User user = mapper.Map<User>(createUserRequest);

            user.HashedPassword = hasherAdapter.HashPassword(createUserRequest.Password);
            user.Role =  Role.Owner.ToString();

            user = await usersHandler.AddUser(user);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            User user = await usersHandler.GetUserByName(loginRequest.Email);

            if (user == null)
            {
                return null;
            }
            bool isPasswordCorrect = true;
            try
            {

                isPasswordCorrect = hasherAdapter.VerifyPassword(loginRequest.Password, user.HashedPassword);
            }
            catch (Exception e)
            {
                return null;
            }
            if (!isPasswordCorrect)
            {
                return null;
            }
            string token = jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Name = user.Name,

                Role = user.Role,
                Token = token
            };
        }
    }
}
