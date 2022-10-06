namespace BetterCinema.Api.Contracts.Sessions
{
    public class GetSessionResponse
    {
        public int SessionId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int MovieId { get; set; }
    }
}
