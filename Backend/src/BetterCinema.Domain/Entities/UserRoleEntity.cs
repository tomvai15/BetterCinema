using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities;

public class UserRoleEntity: IIdentifiable<int>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    
    public UserEntity? User { get; set; }
    public RoleEntity? Role { get; set; }
}