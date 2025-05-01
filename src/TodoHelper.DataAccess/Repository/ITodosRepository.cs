
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;
public interface ITodosRepository
{
    Task CreateCategoryAsync(Category category);
    Task CreateTodoAsync(Todo todo);
    Task DeleteCategoryAsync(Category category);
    Task DeleteTodoAsync(Todo todo);
    IOrderedEnumerable<Category> GetCategories();
    IOrderedEnumerable<Todo> GetCompleteTodos();
    IOrderedEnumerable<Todo> GetImportantTodos();
    IOrderedEnumerable<Todo> GetOverdueTodos();
    IOrderedEnumerable<Todo> GetTodosDueToday();
    Task RenameCategoryAsync(Category category, string name);
    Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId);
    Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate);
    Task UpdateTodoDescriptionAsync(Todo todo, string description);
    Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate);
    Task UpdateTodoImportanceAsync(Todo todo);
    bool CategoryOfSameNameExists(string name);
}
