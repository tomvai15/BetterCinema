using BetterCinema.Api.Providers;
using BetterCinema.Api.Validation.Requirements;
using BetterCinema.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BetterCinema.Api.Validation.Handlers
{
    public class TheaterIdInRouteValidationHandler(
        ITheatersRepository theatersRepository,
        IClaimsProvider claimsProvider,
        IActionContextAccessor actionContextAccessor)
        : AuthorizationHandler<TheaterIdInRouteRequirement>, IAuthorizationHandler
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TheaterIdInRouteRequirement requirement)
        {
            if (!claimsProvider.TryGetUserId(out var userId))
            {
                return;
            }

            if (context.Resource is HttpContext httpContext)
            {
                var theaterId = int.Parse(httpContext.GetRouteValue("theaterId").ToString());
                var theaterEntity = await theatersRepository.GetByIdAsync(theaterId);
                if (theaterEntity == null)
                {
                    return;
                }

                if (theaterEntity.UserId != userId)
                {
                    context.Fail();
                    return;
                }
            }

            context.Succeed(requirement);
            return;
        }
    }
}
