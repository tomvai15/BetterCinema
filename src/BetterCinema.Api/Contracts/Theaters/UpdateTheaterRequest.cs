namespace BetterCinema.Api.Contracts.Theaters
{
    public class UpdateTheaterRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
    }
}
