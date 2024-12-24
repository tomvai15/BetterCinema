using System.Text.Json.Serialization;
using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities
{
    public class MovieEntity : IIdentifiable<int>
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }
  
        public required string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public required string Director { get; set; }

        public int TheaterId { get; set; }

        public required string ImageUrl { get; set; }

        [JsonIgnore]
        public TheaterEntity? TheaterEntity { get; set; }

        [JsonIgnore]
        public ICollection<SessionEntity>? Sessions { get; set; }
    }
}
