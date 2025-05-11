
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public sealed class TodosRepository<T>(TodosDbContext context) : ITodosRepository<T> where T : Entity<T>
{
    private readonly TodosDbContext _context = context;

    public async Task Create(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken = default) => await _context.Set<T>().FindAsync(id, cancellationToken);

    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default) => await _context.Set<T>().ToListAsync(cancellationToken);

    public async Task Update(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
