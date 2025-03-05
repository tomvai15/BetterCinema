namespace BetterCinema.Api.Contracts.Movies
{
    public class GetMoviesResponse
    {
        public IList<GetMovieResponse> Movies { get; set; }
    }
}
