namespace BetterCinema.Api.Contracts.Auth
{
    public class LoginResponse
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
