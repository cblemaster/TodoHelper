
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public sealed class TodosRepository<T>(TodosDbContext context) : ITodosRepository<T> where T : Entity<T>
{
    private readonly TodosDbContext _context = context;

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _context.Set<T>().FindAsync([id], cancellationToken: cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _context.Set<T>().ToListAsync(cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
