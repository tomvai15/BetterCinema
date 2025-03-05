using BetterCinema.Api.Contracts.Roles.Responses;
using BetterCinema.Api.Providers;
using BetterCinema.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterCinema.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    //[Authorize]
    public class RolesController(IClaimsProvider claimsProvider, CinemaDbContext context) : ControllerBase
    {
        [HttpGet("user-permissions")]
        public async Task<GetUserPermissionsResponse> GetUserPermissions()
        {
            //var userId = claimsProvider.GetUserId();
            var userId = 1;

            var userRoles = context.UserRoles.Where(x => x.UserId == userId).ToList();
            var roleIds = userRoles.Select(x => x.RoleId).Distinct().ToList();

            var permissions = context.Permissions.Where(x => roleIds.Contains(x.RoleId)).ToList();


            return new GetUserPermissionsResponse
            {
                Permissions = permissions.Select(x => new UserPermissionResponseModel
                {
                    ResourceType = x.ResourceType,
                    AccessType = x.AccessType,
                }).OrderBy(x => x.ResourceType).ToList(),
            };
        }
    }
}