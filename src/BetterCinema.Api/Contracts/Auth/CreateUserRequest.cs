﻿namespace BetterCinema.Api.Contracts.Auth
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}