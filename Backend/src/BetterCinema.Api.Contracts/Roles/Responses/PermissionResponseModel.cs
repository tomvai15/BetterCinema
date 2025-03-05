namespace BetterCinema.Api.Contracts.Roles.Responses;

public class PermissionResponseModel
{
    public required string ResourceName { get; set; }
    public required int ResourceType { get; set; }
    public required IList<PermissionAccessResponseModel> Accesses { get; set; }
}