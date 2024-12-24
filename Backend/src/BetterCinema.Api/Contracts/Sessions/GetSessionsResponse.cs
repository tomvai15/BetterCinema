using BetterCinema.Api.Contracts.Movies;

namespace BetterCinema.Api.Contracts.Sessions
{
    public class GetSessionsResponse
    {
        public IEnumerable<GetSessionResponse> Sessions { get; set; }
    }
}
