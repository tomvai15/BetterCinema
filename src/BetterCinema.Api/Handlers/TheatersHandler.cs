using BetterCinema.Api.Contracts;
using BetterCinema.Api.Data;
using BetterCinema.Api.Extensions;
using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ITheatersHandler
    {
        public Task<GetTheatersResponse> GetTheaters(int limit, int offset);
    }

    public class TheatersHandler: ITheatersHandler
    {
        private readonly CinemaDbContext context;

        public TheatersHandler(CinemaDbContext context)
        {
            this.context = context;
        }

        public async Task<GetTheatersResponse> GetTheaters(int limit, int offset)
        {
            var totalCount = context.Theaters.Count();

            IEnumerable<Theater> theaters = await context.Theaters.GetSetSection(limit, offset).ToListAsync();

            return new GetTheatersResponse { Theaters = theaters, TotalCount = totalCount };
        }
    }
}
