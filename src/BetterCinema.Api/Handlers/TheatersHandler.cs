using AutoMapper;
using BetterCinema.Api.Constants;
using BetterCinema.Api.Contracts;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Data;
using BetterCinema.Api.Extensions;
using BetterCinema.Api.Models;
using BetterCinema.Api.Providers;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ITheatersHandler
    {
        public Task<GetTheatersResponse> GetTheaters(int limit, int offset);
        public Task<Theater> GetTheater(int theaterId);
    }

    public class TheatersHandler : ITheatersHandler
    {
        private readonly CinemaDbContext context;
        private readonly IMapper mapper;

        private readonly IClaimsProvider claimsProvider;

        public TheatersHandler(CinemaDbContext context, IMapper mapper, IClaimsProvider claimsProvider)
        {
            this.context = context;
            this.mapper = mapper;
            this.claimsProvider = claimsProvider;
        }

        public async Task<Theater> GetTheater(int theaterId)
        {
            return await context.Theaters.FirstOrDefaultAsync(t => t.TheaterId == theaterId);
        }

        public async Task<GetTheatersResponse> GetTheaters(int limit, int offset)
        {
            var totalCount = context.Theaters.Count();

            IEnumerable<Theater> theaters = await context.Theaters.GetSetSection(limit, offset).ToListAsync();     

            claimsProvider.TryGetUserRole(out string role);
            claimsProvider.TryGetUserId(out int userId);

            if (role != Role.Admin)
            {
                theaters = theaters.Where(t => t.IsConfirmed || t.UserId == userId);
            }

            return new GetTheatersResponse { Theaters = mapper.Map<IEnumerable<GetTheaterResponse>>(theaters), TotalCount = totalCount };
        }
    }
}
