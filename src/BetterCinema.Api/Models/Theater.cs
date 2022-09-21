using System.ComponentModel.DataAnnotations;

namespace BetterCinema.Api.Models
{
    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }
    }
}
