using BetterCinema.Api.Contracts.Sessions;

namespace BetterCinema.Api.Contracts.Movies
{
    public class GetMovieResponse
    {
        public int MovieId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int TheaterId { get; set; }
        public string ImageUrl { get; set; }
        public IList<GetSessionResponse> Sessions { get; set; }
    }
}
