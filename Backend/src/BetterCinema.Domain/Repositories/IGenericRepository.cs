using System.Linq.Expressions;
using BetterCinema.Domain.Entities.Base;

namespace BetterCinema.Domain.Repositories;

public interface IGenericRepository<TEntity, TId> : IGenericRepository where TEntity: IIdentifiable<TId>
{
    Task<TEntity?> GetByIdAsync(TId id);
    Task<IList<TEntity>> GetAllAsync();
    Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void Delete(TEntity entity);
}

public interface IGenericRepository
{
    Task SaveChangesAsync();
}
