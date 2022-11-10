using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BetterCinema.Api.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
  
        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        public int TheaterId { get; set; }

        public string ImageUrl { get; set; }

        [JsonIgnore]
        public Theater Theater { get; set; }

        [JsonIgnore]
        public ICollection<Session> Sessions { get; set; }
    }
}
