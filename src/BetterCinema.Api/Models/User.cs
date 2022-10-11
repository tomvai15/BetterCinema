using System.Text.Json.Serialization;

namespace BetterCinema.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public ICollection<Theater> Theaters { get; set; }
    }
}
