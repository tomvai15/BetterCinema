using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BetterCinema.Api.Models
{
    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsConfimed { get; set; }

        [JsonIgnore]
        public ICollection<Movie> Movies { get; set; }
    }
}
