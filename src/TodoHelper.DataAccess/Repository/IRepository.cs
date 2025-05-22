
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public interface IRepository<T> where T : Entity<T>
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Identifier<T> id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    void DisposeEntity(T entity);
}
