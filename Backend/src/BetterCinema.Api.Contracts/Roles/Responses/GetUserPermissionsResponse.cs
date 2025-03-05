namespace BetterCinema.Api.Contracts.Roles.Responses;

public class GetUserPermissionsResponse
{
    public required IList<UserPermissionResponseModel> Permissions { get; set; }
}