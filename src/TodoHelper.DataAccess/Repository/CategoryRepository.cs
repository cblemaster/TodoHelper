
using Microsoft.EntityFrameworkCore;
using TodoHelper.DataAccess.Context;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;

namespace TodoHelper.DataAccess.Repository;

public class CategoryRepository(TodosDbContext context) : IRepository<Category>
{
    private readonly TodosDbContext _context = context;

    public async Task<Category> CreateAsync(Category entity)
    {
        _ = _context.Set<Category>().Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Category?> GetByIdAsync(Guid id) => await _context.Set<Category>().FindAsync(Identifier<Category>.Create(id));

    public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Set<Category>().ToListAsync();

    public async Task UpdateAsync(Category entity)
    {
        _ = _context.Set<Category>().Update(entity);
        _ = await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category entity)
    {
        _ = _context.Set<Category>().Remove(entity);
        _ = await _context.SaveChangesAsync();
    }

    public void DisposeEntity(Category entity) => _context.Entry(entity).State = EntityState.Detached;
}
