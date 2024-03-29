﻿using AutoMapper;
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
        private readonly HasherAdapter hasherAdapter;

        public UserAuthHandler(IJwtTokenGenerator jwtTokenGenerator, IUsersHandler usersHandler, IMapper mapper, HasherAdapter hasherAdapter)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.usersHandler = usersHandler;
            this.mapper = mapper;
            this.hasherAdapter = hasherAdapter;
        }
        public async Task<bool> TryCreateUser(CreateUserRequest createUserRequest)
        {
            User user = mapper.Map<User>(createUserRequest);

            user.HashedPassword = hasherAdapter.HashText(createUserRequest.Password);
            user.Role =  Role.Owner.ToString();

            user = await usersHandler.AddNewUser(user);

            bool isUserCreated = user != null;
            return isUserCreated;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            User user = await usersHandler.GetUserByEmail(loginRequest.Email);

            if (user == null) return null;

            bool isPasswordCorrect = hasherAdapter.VerifyHashedText(loginRequest.Password, user.HashedPassword);
            
            if (!isPasswordCorrect) return null;

            string token = jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Name = user.Name,
                Token = token
            };
        }
    }
}
