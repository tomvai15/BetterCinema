using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts.Auth;
using BetterCinema.Api.Cryptography;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Handlers
{
    public interface IUserAuthHandler
    {
        Task<LoginResponse> LoginUser(LoginRequest loginRequest);
        Task<bool> TryCreateUser(CreateUserRequest createUserRequest);
    }
    public class UserAuthHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUsersHandler usersHandler,
        IMapper mapper,
        HasherAdapter hasherAdapter)
        : IUserAuthHandler
    {
        public async Task<bool> TryCreateUser(CreateUserRequest createUserRequest)
        {
            var userEntity = mapper.Map<UserEntity>(createUserRequest);

            userEntity.HashedPassword = hasherAdapter.HashText(createUserRequest.Password);
            userEntity.Role =  Role.Owner.ToString();

            userEntity = await usersHandler.AddNewUser(userEntity);

            var isUserCreated = userEntity != null;
            return isUserCreated;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            var userEntity = await usersHandler.GetUserByEmail(loginRequest.Email);

            if (userEntity == null) return null;

            var isPasswordCorrect = hasherAdapter.VerifyHashedText(loginRequest.Password, userEntity.HashedPassword);
            
            if (!isPasswordCorrect) return null;

            var token = jwtTokenGenerator.GenerateToken(userEntity);

            return new LoginResponse
            {
                Name = userEntity.Name,
                Token = token
            };
        }
    }
}
