using BetterCinema.Api.Handlers;
using BetterCinema.Api.Providers;
using BetterCinema.Api.Validation.Requirements;
using BetterCinema.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BetterCinema.Api.Validation.Handlers
{
    public class TheaterIdInRouteValidationHandler(
        ITheatersHandler theatersHandler,
        IClaimsProvider claimsProvider,
        IActionContextAccessor actionContextAccessor)
        : AuthorizationHandler<TheaterIdInRouteRequirement>, IAuthorizationHandler
    {
        private readonly IActionContextAccessor actionContextAccessor = actionContextAccessor;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TheaterIdInRouteRequirement requirement)
        {
            if (!claimsProvider.TryGetUserId(out var userId))
            {
                return;
            }

            if (context.Resource is HttpContext httpContext)
            {
                var theaterId = int.Parse(httpContext.GetRouteValue("theaterId").ToString());
                var theaterEntity = await theatersHandler.GetTheater(theaterId);
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
