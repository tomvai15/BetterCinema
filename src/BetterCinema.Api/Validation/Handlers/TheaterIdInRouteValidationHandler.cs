using BetterCinema.Api.Handlers;
using BetterCinema.Api.Models;
using BetterCinema.Api.Providers;
using BetterCinema.Api.Validation.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BetterCinema.Api.Validation.Handlers
{
    public class TheaterIdInRouteValidationHandler : AuthorizationHandler<TheaterIdInRouteRequirement>, IAuthorizationHandler
    {
        private readonly ITheatersHandler theatersHandler;
        private readonly IClaimsProvider claimsProvider;
        private readonly IActionContextAccessor actionContextAccessor;
        public TheaterIdInRouteValidationHandler(ITheatersHandler theatersHandler, IClaimsProvider claimsProvider, IActionContextAccessor actionContextAccessor)
        {
            this.theatersHandler = theatersHandler;
            this.claimsProvider = claimsProvider;
            this.actionContextAccessor = actionContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TheaterIdInRouteRequirement requirement)
        {
            if (!claimsProvider.TryGetUserId(out int userId))
            {
                return;
            }

            if (context.Resource is HttpContext httpContext)
            {
                int theaterId = int.Parse(httpContext.GetRouteValue("theaterId").ToString());
                Theater theater = await theatersHandler.GetTheater(theaterId);
                if (theater == null)
                {
                    return;
                }

                if (theater.UserId != userId)
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
