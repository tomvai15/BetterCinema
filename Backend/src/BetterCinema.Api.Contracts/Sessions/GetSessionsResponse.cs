using BetterCinema.Api.Contracts.Movies;

namespace BetterCinema.Api.Contracts.Sessions
{
    public class GetSessionsResponse
    {
        public IList<GetSessionResponse> Sessions { get; set; }
    }
}
