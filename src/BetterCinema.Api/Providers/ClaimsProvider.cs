using AutoMapper;
using BetterCinema.Api.Data;
using BetterCinema.Api.Handlers;

namespace BetterCinema.Api.Providers
{
    public interface IClaimsProvider
    {
        bool TryGetUserId(out int userId);
        bool TryGetUserRole(out string role);
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
                userId = -1;
                return false;
            }
            return int.TryParse(claim.Value, out userId);
        }

        public bool TryGetUserRole(out string role)
        {
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (claim == null)
            {
                role = "";
                return false;
            }
            role = claim.Value.ToString();
            return true;
        }
    }
}
