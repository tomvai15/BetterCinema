using AutoMapper;
using BetterCinema.Api.Contracts.Movies;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ISessionsHandler
    {
        Task<IEnumerable<Session>> GetSessions(int theaterId, int movieId);
        Task<Session> GetSession(int theaterId, int movieId, int sessionId);
        Task<Session> UpdateSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest);
        Task<Session> CreateSession(int theaterId, int movieId, CreateSessionRequest createSessionRequest);
        Task<bool> DeleteSession(int theaterId, int movieId, int sessionId);
    }
    public class SessionsHandler : ISessionsHandler
    {
        private readonly IMoviesHandler moviesHandler;
        private readonly CinemaDbContext context;
        private readonly IMapper mapper;

        public SessionsHandler(IMoviesHandler moviesHandler, CinemaDbContext context, IMapper mapper)
        {
            this.moviesHandler = moviesHandler;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Session>> GetSessions(int theaterId, int movieId)
        {
            bool movieExists = (await moviesHandler.GetMovie(theaterId, movieId)) != null;

            if (!movieExists)
            {
                return null;
            }

            Movie movie = await context.Movies.Where(t => t.MovieId == movieId)
                .Include(t => t.Sessions)
                .FirstAsync();

            return movie.Sessions.ToList();
        }
        public async Task<Session> GetSession(int theaterId, int movieId, int sessionId)
        {
            Movie movie = await moviesHandler.GetMovie(theaterId, movieId);
            if (movie == null)
            {
                return null;
            }
            Session session = await context.Sessions.FindAsync(sessionId);
            if (session == null)
            {
                return null;
            }

            if (session.MovieId != movie.MovieId)
            {
                return null;
            }

            return session;
        }

        public async Task<Session> CreateSession(int theaterId, int movieId, CreateSessionRequest createSessionRequest)
        {
            Movie movie = await moviesHandler.GetMovie(theaterId, movieId);

            if (movie == null)
            {
                return null;
            }

            Session newSession = mapper.Map<Session>(createSessionRequest);
            newSession.MovieId = movieId;
            var session = context.Sessions.Add(newSession);
            await context.SaveChangesAsync();
            return session.Entity;
        }

        public async Task<Session> UpdateSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest)
        {
            Session session = await GetSession(theaterId, movieId, sessionId);
            if (session == null)
            {
                return null;
            }
            if ( session.MovieId != movieId)
            {
                return null;
            }

            session.Start = updateSessionRequest.Start;
            session.End = updateSessionRequest.End;
            session.Hall = updateSessionRequest.Hall;

            context.Entry(session).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> DeleteSession(int theaterId, int movieId, int sessionId)
        {
            Session session = await GetSession(theaterId, movieId, sessionId);
            if (session == null)
            {
                return false;
            }

            context.Sessions.Remove(session);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
