
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;

// NOTE: For a project of this size and complexity, a repository abstraction is overkill
// ...extending the db context would work just as well with less complexity
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

    public async Task UpdateCategoryAsync(Category category)
    {
        _ = _context.Update(category);
        await SaveAsync();
    }
    public async Task UpdateTodoAsync(Todo todo)
    {
        _ = _context.Update(todo);
        await SaveAsync();
    }
    public async Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId)
    {
        todo.SetCategoryId(categoryId);
        await UpdateTodoAsync(todo);
    }
    public async Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate)
    {
        todo.SetDueDate(dueDate);
        await UpdateTodoAsync(todo);
    }
    public async Task UpdateTodoImportanceAsync(Todo todo)
    {
        todo.SetImportance();
        await UpdateTodoAsync(todo);
    }
    public async Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate)
    {
        todo.SetCompleteDate(completeDate);
        await UpdateTodoAsync(todo);
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

    public IEnumerable<Category> GetCategories() => _context.Categories;
    public IEnumerable<Todo> GetTodos() => _context.Todos;
    public Todo? GetTodoById(Guid id) => _context.Todos.Single(t => t.Id.Value == id);
    public Category? GetCategoryById(Guid id) => _context.Categories.Single(c => c.Id.Value == id);

    private async Task SaveAsync() => await _context.SaveChangesAsync();

    // NOTE: This method does not really belong here
    // ...ideally I would inject the repository (or even the db context) into a validator
    // ...the tradeoff is that we get to have very simple validation
    public bool CategoryOfSameNameExists(string name) => _context.Categories.Select(c => c.Name.Value).ToList().Contains(name);
}
