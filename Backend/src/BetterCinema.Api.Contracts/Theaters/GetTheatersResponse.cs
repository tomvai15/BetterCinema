namespace BetterCinema.Api.Contracts.Theaters
{
    public class GetTheatersResponse
    {
        public IList<GetTheaterResponse> Theaters { get; set; }
        public int TotalCount { get; set; }
    }
}
