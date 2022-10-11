using System.ComponentModel.DataAnnotations;

namespace BetterCinema.Api.Contracts.Auth
{
    public class CreateUserRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
    }
}
