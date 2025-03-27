using System.Linq.Expressions;
using BetterCinema.Domain.Entities.Base;
using BetterCinema.Domain.Repositories;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity, TId>(CinemaDbContext context)
    : IGenericRepository<TEntity, TId> where TEntity : class, IIdentifiable<TId>
{
    private readonly DbSet<TEntity> _entities = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<IList<TEntity>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync();
    }

    public void Add(TEntity entity)
    {
        _entities.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}