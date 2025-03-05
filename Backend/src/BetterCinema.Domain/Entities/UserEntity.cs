using System.Text.Json.Serialization;
using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities
{
    public class UserEntity : IIdentifiable<int>
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }

        [JsonIgnore]
        public required string HashedPassword { get; set; }
        public required string Role { get; set; }

        [JsonIgnore]
        public ICollection<TheaterEntity>? Theaters { get; set; }
        public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}
