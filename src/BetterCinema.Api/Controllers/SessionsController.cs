﻿using Microsoft.AspNetCore.Mvc;
using BetterCinema.Api.Models;
using AutoMapper;
using BetterCinema.Api.Handlers;
using BetterCinema.Api.Contracts.Sessions;
using BetterCinema.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies/{movieId}/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionsHandler sessionsHandler;
        private readonly IMapper mapper;

        public SessionsController(ISessionsHandler sessionsHandler, IMapper mapper)
        {
            this.mapper = mapper;
            this.sessionsHandler = sessionsHandler;
        }

        [HttpGet]
        public async Task<ActionResult<GetSessionsResponse>> GetSessions(int theaterId, int movieId)
        {
            IEnumerable<Session> sessions = await sessionsHandler.GetSessions(theaterId, movieId);

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
            Session session = await sessionsHandler.GetSession(theaterId, movieId, sessionId);

            if (session == null)
            {
                return NotFound();
            }

            return mapper.Map<GetSessionResponse>(session);
        }

        [HttpPatch("{sessionId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<IActionResult> PatchSession(int theaterId, int movieId, int sessionId, UpdateSessionRequest updateSessionRequest)
        {
            bool wasSessionUpdated = await sessionsHandler.UpdateSession(theaterId, movieId, sessionId, updateSessionRequest);
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
            Session session = await sessionsHandler.CreateSession(theaterId, movieId, createSessionRequest);

            if (session == null)
            {
                return NotFound();
            }

            GetSessionResponse sessionResponse = mapper.Map<GetSessionResponse>(session);

            return CreatedAtAction("GetSession", new { theaterId = theaterId, movieId = movieId, sessionId = session.SessionId }, sessionResponse);
        }

        [HttpDelete("{sessionId}")]
        [Authorize(Roles = Role.Owner)]
        [Authorize(Policy = AuthPolicy.TheaterIdInRouteValidation)]
        public async Task<ActionResult> DeleteSession(int theaterId, int movieId, int sessionId)
        {

            bool wasSessionDeleted = await sessionsHandler.DeleteSession(theaterId, movieId, sessionId);
            if (!wasSessionDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
