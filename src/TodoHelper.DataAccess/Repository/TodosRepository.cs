
using Microsoft.EntityFrameworkCore;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;

public class TodosRepository(TodosDbContext context) : ITodosRepository
{
    private readonly TodosDbContext _context = context;

    public void CreateCategory(Category category)
    {
        _ = _context.Categories.Add(category);
        Save();
    }
    public void CreateTodo(Todo todo)
    {
        _ = _context.Todos.Add(todo);
        Save();
    }

    public void RenameCategory(Category category, string name)
    {
        category.Rename(name);
        _ = _context.Update(category);
        Save();
    }
    public void UpdateTodoDescription(Todo todo, string description)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetDescription(description);
            _ = _context.Update(todo);
            Save();
        }
    }
    public void UpdateTodoCategory(Todo todo, Identifier<Category> categoryId)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetCategoryId(categoryId);
            _ = _context.Update(todo);
            Save();
        }
    }
    public void UpdateTodoDueDate(Todo todo, DateOnly? dueDate)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetDueDate(dueDate);
            _ = _context.Update(todo);
            Save();
        }
    }
    public void UpdateTodoImportance(Todo todo)
    {
        if (todo.CanBeUpdated)
        {
            todo.SetImportance();
            _ = _context.Update(todo);
            Save();
        }
    }
    public void UpdateTodoCompleteDate(Todo todo, DateTimeOffset? completeDate)
    {
        todo.SetCompleteDate(completeDate);
        _ = _context.Update(todo);
        Save();
    }

    public void DeleteCategory(Category category)
    {
        if (category.CanBeDeleted && _context.Categories.Contains(category))
        {
            _ = _context.Categories.Remove(category);
            Save();
        }
    }
    public void DeleteTodo(Todo todo)
    {
        if (todo.CanBeDeleted && _context.Todos.Contains(todo))
        {
            _ = _context.Todos.Remove(todo);
            Save();
        }
    }

    private void Save() => _context.SaveChanges();

    public IOrderedEnumerable<Category> GetCategories() => _context.Categories.AsEnumerable().OrderBy(c => c.Name);
    private IEnumerable<Todo> GetNotCompleteTodos() => _context.Todos.Where(t => !t.IsComplete);
    public IOrderedEnumerable<Todo> GetCompleteTodos() => _context.Todos.Where(t => t.IsComplete).AsEnumerable().OrderByDescending(t => t.CompleteDate).ThenBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetImportantTodos() => GetNotCompleteTodos().Where(t => t.Importance.IsImportant).AsEnumerable().OrderByDescending(t => t.DueDate).ThenBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetTodosDueToday() => GetNotCompleteTodos().Where(t => t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today)).AsEnumerable().OrderBy(t => t.Description.Value);
    public IOrderedEnumerable<Todo> GetOverdueTodos() => GetNotCompleteTodos().Where(t => t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today)).AsEnumerable().OrderByDescending(t => t.DueDate).ThenBy(t => t.Description.Value);
}
