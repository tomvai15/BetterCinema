using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetterCinema.Api.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int MovieId { get;set; }

        [JsonIgnore]
        public Movie Movie { get; set; }
    }
}
