namespace BetterCinema.Api.Contracts.Sessions
{
    public class CreateSessionRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int MovieId { get; set; }
    }
}
