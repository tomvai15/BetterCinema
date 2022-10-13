namespace BetterCinema.Api.Contracts.Theaters
{
    public class CreateTheaterRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
