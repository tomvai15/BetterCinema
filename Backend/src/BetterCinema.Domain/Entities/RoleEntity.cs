using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities;

public class RoleEntity : IIdentifiable<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<UserRoleEntity>? UserRoles { get; set; }
    public ICollection<PermissionEntity>? Permissions { get; set; }
}