using BetterCinema.Domain.Entities.Base;
using BetterCinema.Domain.Enums;

namespace BetterCinema.Domain.Entities;

public class PermissionEntity: IIdentifiable<int>
{
    public int Id { get; set; }
    public int AccessType { get; set; }
    public int ResourceType { get; set; }
    
    public int RoleId { get; set; }
    public RoleEntity? Role { get; set; }
}