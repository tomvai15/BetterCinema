namespace BetterCinema.Api.Contracts.Movies
{
    public class CreateMovieRequest
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public DateTime RealeaseDate { get; set; }

        public string Director { get; set; }
    }
}
