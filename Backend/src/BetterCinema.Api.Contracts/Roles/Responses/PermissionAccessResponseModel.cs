namespace BetterCinema.Api.Contracts.Roles.Responses;

public class PermissionAccessResponseModel
{
    public int AccessType { get; set; }
    public required string Name { get; set; }
    public bool HasAccess { get; set; }
}