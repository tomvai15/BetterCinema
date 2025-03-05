using System.Text.Json.Serialization;
using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities
{
    public class TheaterEntity : IIdentifiable<int>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsConfirmed { get; set; }

        [JsonIgnore] public ICollection<MovieEntity>? Movies { get; set; }

        public int UserId { get; set; }

        [JsonIgnore] public UserEntity? User { get; set; }
    }
}