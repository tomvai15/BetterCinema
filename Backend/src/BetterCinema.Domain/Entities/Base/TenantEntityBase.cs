namespace BetterCinema.Domain.Entities.Base;

public abstract class TenantEntityBase<T>: IIdentifiable<T>
{
    public required T Id { get; set; }
    public int TenantId { get; set; }
    public TenantEntity? Tenant { get; set; }
}