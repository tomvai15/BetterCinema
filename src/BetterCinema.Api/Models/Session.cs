using System.ComponentModel.DataAnnotations;

namespace BetterCinema.Api.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
