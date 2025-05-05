
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;

public sealed class TodosRepository(TodosDbContext context) : ITodosRepository
{
    private readonly TodosDbContext _context = context;

    public async Task CreateCategoryAsync(Category category)
    {
        _ = _context.Categories.Add(category);
        await SaveAsync();
    }
    public async Task CreateTodoAsync(Todo todo)
    {
        _ = _context.Todos.Add(todo);
        await SaveAsync();
    }

    public async Task UpdateCategoryNameAsync(Category category, string name)
    {
        category.SetName(name);
        _ = _context.Update(category);
        await SaveAsync();
    }
    public async Task UpdateTodoDescriptionAsync(Todo todo, string description)
    {
        todo.SetDescription(description);
        _ = _context.Update(todo);
        await SaveAsync();
    }
    public async Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId)
    {
        todo.SetCategoryId(categoryId);
        _ = _context.Update(todo);
        await SaveAsync();
    }
    public async Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate)
    {
        todo.SetDueDate(dueDate);
        _ = _context.Update(todo);
        await SaveAsync();
    }
    public async Task UpdateTodoImportanceAsync(Todo todo)
    {
        todo.SetImportance();
        _ = _context.Update(todo);
        await SaveAsync();
    }
    public async Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate)
    {
        todo.SetCompleteDate(completeDate);
        _ = _context.Update(todo);
        await SaveAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _ = _context.Categories.Remove(category);
        await SaveAsync();
    }
    public async Task DeleteTodoAsync(Todo todo)
    {
        _ = _context.Todos.Remove(todo);
        await SaveAsync();
    }

    public IEnumerable<Category> GetCategories() => _context.Categories.Include(c => c.Todos);
    public IEnumerable<Todo> GetTodos() => _context.Todos.Include(t => t.Category);

    private async Task SaveAsync() => await _context.SaveChangesAsync();

    public bool CategoryOfSameNameExists(string name) => _context.Categories.Select(c => c.Name.Value).ToList().Contains(name);
}
