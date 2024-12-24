using BetterCinema.Api.Contracts.Theaters;

namespace BetterCinema.Api.Contracts
{
    public class GetTheatersResponse
    {
        public IEnumerable<GetTheaterResponse> Theaters { get; set; }
        public int TotalCount { get; set; }
    }
}
