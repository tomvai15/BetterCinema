using System.ComponentModel.DataAnnotations;

namespace BetterCinema.Api.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Name { get; set; }
    }
}
