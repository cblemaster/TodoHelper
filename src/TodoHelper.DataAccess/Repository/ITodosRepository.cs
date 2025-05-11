
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public interface ITodosRepository<T> where T : Entity<T>
{
    Task Create(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
    Task Update(T entity, CancellationToken cancellationToken = default);
    Task Delete(T entity, CancellationToken cancellationToken = default);
}
