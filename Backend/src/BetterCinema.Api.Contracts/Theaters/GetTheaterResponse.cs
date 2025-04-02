namespace BetterCinema.Api.Contracts.Theaters
{
    public class GetTheaterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsConfirmed { get; set; }

        public int UserId { get; set; }

        public bool IsOwned { get; set; }
    }
}
