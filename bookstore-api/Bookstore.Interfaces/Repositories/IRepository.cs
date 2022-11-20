using System.Linq.Expressions;

namespace Bookstore.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetByIdAsync(Guid id);

    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> SignleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
