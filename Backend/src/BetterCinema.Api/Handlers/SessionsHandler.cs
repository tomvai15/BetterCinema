using AutoMapper;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ISessionsHandler
    {
        Task<IEnumerable<SessionEntity>> GetSessions(int theaterId, int movieId);
        Task<SessionEntity> GetSession(int theaterId, int movieId, int sessionId);
        Task<bool> UpdateSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest);
        Task<SessionEntity> CreateSession(int theaterId, int movieId, CreateSessionRequest createSessionRequest);
        Task<bool> DeleteSession(int theaterId, int movieId, int sessionId);
    }
    public class SessionsHandler(IMoviesRepository moviesRepository, CinemaDbContext context, IMapper mapper)
        : ISessionsHandler
    {
        public async Task<IEnumerable<SessionEntity>> GetSessions(int theaterId, int movieId)
        {
            var movieExists = (await moviesRepository.GetMovie(theaterId, movieId)) != null;

            if (!movieExists) return null;

            var movieEntity = await context.Movies.Where(t => t.Id == movieId)
                .Include(t => t.Sessions)
                .FirstAsync();

            return movieEntity.Sessions.ToList();
        }
        public async Task<SessionEntity> GetSession(int theaterId, int movieId, int sessionId)
        {
            var movieEntity = await moviesRepository.GetMovie(theaterId, movieId);
            if (movieEntity == null) return null;
            
            var sessionEntity = await context.Sessions.FindAsync(sessionId);
            if (sessionEntity == null) return null;
            
            if (sessionEntity.MovieId != movieEntity.Id) return null;

            return sessionEntity;
        }

        public async Task<SessionEntity> CreateSession(int theaterId, int movieId, CreateSessionRequest createSessionRequest)
        {
            var movieEntity = await moviesRepository.GetMovie(theaterId, movieId);

            if (movieEntity == null) return null;

            var newSessionEntity = mapper.Map<SessionEntity>(createSessionRequest);
            newSessionEntity.MovieId = movieId;
            var session = context.Sessions.Add(newSessionEntity);
            await context.SaveChangesAsync();
            return session.Entity;
        }

        public async Task<bool> UpdateSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest)
        {
            var sessionEntity = await GetSession(theaterId, movieId, sessionId);

            if (sessionEntity == null) return false;

            if (sessionEntity.MovieId != movieId) return false;

            sessionEntity.Start = updateSessionRequest.Start;
            sessionEntity.End = updateSessionRequest.End;
            sessionEntity.Hall = updateSessionRequest.Hall;

            context.Entry(sessionEntity).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSession(int theaterId, int movieId, int sessionId)
        {
            var sessionEntity = await GetSession(theaterId, movieId, sessionId);
            if (sessionEntity == null) return false;

            context.Sessions.Remove(sessionEntity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
