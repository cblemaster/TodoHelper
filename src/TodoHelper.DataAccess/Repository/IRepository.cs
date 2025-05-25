
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public interface IRepository<T> where T : Entity<T>
{
    // NOTE: This generic interface is flexible, but this comes
    //   at a cost in concrete implementations:
    //   filtering, sorting, and includes cannot be
    //   performed by the repo as they are type-specific, leaving these concerns to
    //   the caller instead, which is not ideal
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Identifier<T> id);
    Task<IEnumerable<T>> GetAllEnumerable();
    IQueryable<T> GetAllAsyncQueryable();
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
