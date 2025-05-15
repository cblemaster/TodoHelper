
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public sealed class Repository<T>(TodosDbContext context) : IRepository<T> where T : Entity<T>
{
    private readonly TodosDbContext _context = context;

    public async Task<T> CreateAsync(T entity)
    {
        _ = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(Identifier<T>.Create(id));

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task UpdateAsync(T entity)
    {
        _ = _context.Set<T>().Update(entity);
        _ = await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _ = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync();
    }
}
