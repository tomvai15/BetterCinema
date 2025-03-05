using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Entities;

public class TenantEntity: IIdentifiable<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
}