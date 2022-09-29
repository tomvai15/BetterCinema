using BetterCinema.Api.Models;

namespace BetterCinema.Api.Contracts
{
    public class GetTheatersResponse
    {
        public IEnumerable<Theater> Theaters { get; set; }
        public int TotalCount { get; set; }
    }
}
