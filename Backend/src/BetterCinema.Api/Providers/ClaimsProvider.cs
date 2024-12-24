using BetterCinema.Api.Constants;
using System.Security.Claims;

namespace BetterCinema.Api.Providers
{
    public interface IClaimsProvider
    {
        bool TryGetUserId(out int userId);
        bool TryGetUserRole(out string role);
    }

    public class ClaimsProvider(IHttpContextAccessor httpContextAccessor) : IClaimsProvider
    {
        public bool TryGetClaim(string type, out string value)
        {
            value = "";
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == type);
            if (claim == null)
            {
                return false;
            }
            value = claim.Value;
            return true;
        }

        public bool TryGetUserId(out int userId)
        {
            userId = 0;
            var wasFound =  TryGetClaim(CustomClaim.UserId, out var value);
            if (!wasFound)
            {
                return false;
            }
            return int.TryParse(value, out userId);
        }

        public bool TryGetUserRole(out string role)
        {
            role = "";
            return TryGetClaim(ClaimTypes.Role, out role);
        }
    }
}
