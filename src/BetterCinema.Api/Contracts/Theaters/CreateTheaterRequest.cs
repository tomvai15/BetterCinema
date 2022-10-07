namespace BetterCinema.Api.Contracts.Theaters
{
    public class CreateTheaterRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public bool IsConfimed { get; set; }
    }
}
