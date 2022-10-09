using AutoMapper;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;

namespace BetterCinema.Api.Providers
{
    public interface IClaimsProvider
    {
        bool TryGetUserId(out int userId);
    }

    public class ClaimsProvider: IClaimsProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClaimsProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool TryGetUserId(out int userId)
        {
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (claim == null)
            {
                userId = 0;
                return false;
            }
            return int.TryParse(claim.Value, out userId);
        }
    }
}
