
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;

public class TodosRepository(TodosDbContext context) : ITodosRepository
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

    public async Task RenameCategoryAsync(Category category, string name)
    {
        category.Rename(name);
        _ = _context.Update(category);
        await SaveAsync();
    }
    public async Task UpdateTodoDescriptionAsync(Todo todo, string description)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetDescription(description);
            _ = _context.Update(todo);
            await SaveAsync();
        }
    }
    public async Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetCategoryId(categoryId);
            _ = _context.Update(todo);
            await SaveAsync();
        }
    }
    public async Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetDueDate(dueDate);
            _ = _context.Update(todo);
            await SaveAsync();
        }
    }
    public async Task UpdateTodoImportanceAsync(Todo todo)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetImportance();
            _ = _context.Update(todo);
            await SaveAsync();
        }
    }
    public async Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate)
    {
        todo.SetCompleteDate(completeDate);
        _ = _context.Update(todo);
        await SaveAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        if (category.CanBeDeleted && _context.Categories.Contains(category))
        {
            _ = _context.Categories.Remove(category);
            await SaveAsync();
        }
    }
    public async Task DeleteTodoAsync(Todo todo)
    {
        if (todo.CanBeDeleted && _context.Todos.Contains(todo))
        {
            _ = _context.Todos.Remove(todo);
            await SaveAsync();
        }
    }

    private async Task SaveAsync() => await _context.SaveChangesAsync();

    public IOrderedEnumerable<Category> GetCategories() => _context.Categories.AsEnumerable().OrderBy(c => c.Name);
    private IEnumerable<Todo> GetNotCompleteTodos() => _context.Todos.Where(t => !t.IsComplete);
    public IOrderedEnumerable<Todo> GetCompleteTodos() => _context.Todos.Where(t => t.IsComplete).AsEnumerable().OrderByDescending(t => t.CompleteDate).ThenBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetImportantTodos() => GetNotCompleteTodos().Where(t => t.Importance.IsImportant).AsEnumerable().OrderByDescending(t => t.DueDate).ThenBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetTodosDueToday() => GetNotCompleteTodos().Where(t => t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today)).AsEnumerable().OrderBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetOverdueTodos() => GetNotCompleteTodos().Where(t => t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today)).AsEnumerable().OrderByDescending(t => t.DueDate).ThenBy(t => t.Description.Value);
}
