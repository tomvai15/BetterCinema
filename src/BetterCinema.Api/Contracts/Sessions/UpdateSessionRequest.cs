namespace BetterCinema.Api.Contracts.Sessions
{
    public class UpdateSessionRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Hall { get; set; }
    }
}
