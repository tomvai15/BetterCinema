﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterCinema.Api.Data;
using BetterCinema.Api.Models;

namespace BetterCinema.Api.Controllers
{
    [Route("api/theaters/{theaterId}/movies/{movieId}/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly CinemaDbContext _context;

        public SessionsController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            return await _context.Sessions.ToListAsync();
        }

        // GET: api/Sessions/5
        [HttpGet("{sessionId}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{sessionId}")]
        public async Task<IActionResult> PutSession(int id, Session session)
        {
            if (id != session.SessionId)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.SessionId }, session);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.SessionId == id);
        }
    }
}
