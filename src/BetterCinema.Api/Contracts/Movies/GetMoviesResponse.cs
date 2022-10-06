using BetterCinema.Api.Models;

namespace BetterCinema.Api.Contracts.Movies
{
    public class GetMoviesResponse
    {
        public IEnumerable<GetMovieResponse> Movies { get; set; }
    }
}
