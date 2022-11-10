using BetterCinema.Api.Constants;
using System.Security.Claims;

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
            userId = 0;
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaim.UserId);
            if (claim == null)
            {
                return false;
            }
            return int.TryParse(claim.Value, out userId);
        }

        public bool TryGetUserRole(out string role)
        {
            role = "";
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (claim == null)
            {
                return false;
            }
            role = claim.Value.ToString();
            return true;
        }
    }
}
