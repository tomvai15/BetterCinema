using System.ComponentModel.DataAnnotations;

namespace BetterCinema.Api.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string Director { get; set; }
    }
}
