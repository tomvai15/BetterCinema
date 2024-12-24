namespace BetterCinema.Api.Contracts.Movies
{
    public class CreateMovieRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public string Director { get; set; }
    }
}
