
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;

namespace TodoHelper.DataAccess.Repository;

public sealed class Repository<T>(TodoHelperDbContext context) : IRepository<T> where T : Entity<T>
{
    private readonly TodoHelperDbContext _context = context;

    public async Task<T> CreateAsync(T entity)
    {
        _ = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> GetByIdAsync(Identifier<T> id) => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAllEnumerable() => await _context.Set<T>().ToListAsync();
    
    public IQueryable<T> GetAllAsyncQueryable() => _context.Set<T>().AsQueryable();

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
