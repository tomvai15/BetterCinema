using AutoMapper;
using BetterCinema.Api.Contracts;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Extensions;
using BetterCinema.Api.Providers;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ITheatersHandler
    {
        public Task<GetTheatersResponse> GetTheaters(int limit, int offset);
        public Task<TheaterEntity> GetTheater(int theaterId);
    }

    public class TheatersHandler(CinemaDbContext context, IMapper mapper, IClaimsProvider claimsProvider)
        : ITheatersHandler
    {
        public async Task<TheaterEntity> GetTheater(int theaterId)
        {
            return await context.Theaters.FirstOrDefaultAsync(t => t.Id == theaterId);
        }

        public async Task<GetTheatersResponse> GetTheaters(int limit, int offset)
        {
            var totalCount = context.Theaters.Count();

            IEnumerable<TheaterEntity> theaters = await context.Theaters.GetSetSection(limit, offset).ToListAsync();     

            claimsProvider.TryGetUserRole(out var role);
            claimsProvider.TryGetUserId(out var userId);

            if (role != Role.Admin)
            {
                theaters = theaters.Where(t => t.IsConfirmed || t.UserId == userId);
            }

            return new GetTheatersResponse { Theaters = mapper.Map<IList<GetTheaterResponse>>(theaters), TotalCount = totalCount };
        }
    }
}
