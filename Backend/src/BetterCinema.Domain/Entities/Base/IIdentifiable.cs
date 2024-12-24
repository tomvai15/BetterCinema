namespace BetterCinema.Domain.Entities.Base;

public interface IIdentifiable<T>
{
    public T Id { get; set; }
}

public interface IIdentifiable;