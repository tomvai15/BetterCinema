using System.Text.Json.Serialization;
using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities
{
    public class SessionEntity : IIdentifiable<int>
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public required string Hall { get; set; }

        public int MovieId { get;set; }

        [JsonIgnore]
        public MovieEntity? MovieEntity { get; set; }
    }
}
