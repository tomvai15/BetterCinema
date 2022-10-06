using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Handlers
{
    public interface ISessionsHandler
    {
        Task<IEnumerable<Movie>> GetSessions(int theaterId, int movieId);
        Task<Movie> GetSession(int theaterId, int movieId, int sessionId );
        Task<Movie> UpdateSession(int theaterId, int movieId, UpdateMovieRequest updateMovieRequest);
        Task<Movie> CreateSession(int theaterId, CreateMovieRequest createMovieRequest);

        Task<bool> DeleteMovie(int theaterId, int movieId);
    }
    public class SessionsHandler
    {
    }
}
