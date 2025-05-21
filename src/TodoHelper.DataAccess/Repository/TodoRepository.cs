
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;

namespace TodoHelper.DataAccess.Repository;

internal class TodoRepository(TodosDbContext context) : IRepository<Todo>
{
    private readonly TodosDbContext _context = context;

    public async Task<Todo> CreateAsync(Todo entity)
    {
        _ = _context.Set<Todo>().Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Todo?> GetByIdAsync(Guid id) => await _context.Set<Todo>().FindAsync(Identifier<Category>.Create(id));

    public async Task<IEnumerable<Todo>> GetAllAsync() => await _context.Set<Todo>().ToListAsync();

    public async Task UpdateAsync(Todo entity)
    {
        _ = _context.Set<Todo>().Update(entity);
        _ = await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Todo entity)
    {
        _ = _context.Set<Todo>().Remove(entity);
        _ = await _context.SaveChangesAsync();
    }

    public void DisposeEntity(Todo entity) => _context.Entry(entity).State = EntityState.Detached;
}
