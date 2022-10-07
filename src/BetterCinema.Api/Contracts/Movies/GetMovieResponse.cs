using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Contracts.Movies
{
    public class GetMovieResponse
    {
        public int MovieId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public string Genre { get; set; }

        public DateTime RealeaseDate { get; set; }

        public string Director { get; set; }

        public int TheaterId { get; set; }

        public IEnumerable<GetSessionResponse> Sessions { get; set; }
    }
}
