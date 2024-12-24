using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Constants;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies/{movieId}/sessions")]
    [ApiController]
    public class SessionsController(ISessionsHandler sessionsHandler, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetSessionsResponse>> GetSessions(int theaterId, int movieId)
        {
            IEnumerable<SessionEntity> sessions = await sessionsHandler.GetSessions(theaterId, movieId);

            if (sessions == null)
            {
                return NotFound();
            }

            IEnumerable<GetSessionResponse> sessionsResponse = mapper.Map<IEnumerable<GetSessionResponse>>(sessions);
            return new GetSessionsResponse { Sessions = sessionsResponse };
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<GetSessionResponse>> GetSession(int theaterId, int movieId, int sessionId)
        {
            var sessionEntity = await sessionsHandler.GetSession(theaterId, movieId, sessionId);

            if (sessionEntity == null)
            {
                return NotFound();
            }

            return mapper.Map<GetSessionResponse>(sessionEntity);
        }

        [HttpPatch("{sessionId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PatchSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest)
        {
            var wasSessionUpdated = await sessionsHandler.UpdateSession(theaterId, movieId, sessionId, updateSessionRequest);
            if (!wasSessionUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PostSession(int theaterId, int movieId, CreateSessionRequest createSessionRequest)
        {
            var sessionEntity = await sessionsHandler.CreateSession(theaterId, movieId, createSessionRequest);

            if (sessionEntity == null)
            {
                return NotFound();
            }

            var sessionResponse = mapper.Map<GetSessionResponse>(sessionEntity);

            return CreatedAtAction("GetSession", new { theaterId = theaterId, movieId = movieId, sessionId = sessionEntity.Id }, sessionResponse);
        }

        [HttpDelete("{sessionId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<ActionResult> DeleteSession(int theaterId, int movieId, int sessionId)
        {

            var wasSessionDeleted = await sessionsHandler.DeleteSession(theaterId, movieId, sessionId);
            if (!wasSessionDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
