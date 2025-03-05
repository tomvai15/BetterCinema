namespace BetterCinema.Api.Contracts.Roles.Responses;

public class RoleResponseModel
{
    public required string Name { get; set; }
    public required IList<PermissionResponseModel> Permissions { get; set; }
}