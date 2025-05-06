
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;
public interface ITodosRepository
{
    Task CreateCategoryAsync(Category category);
    Task CreateTodoAsync(Todo todo);
    Task DeleteCategoryAsync(Category category);
    Task DeleteTodoAsync(Todo todo);
    Task UpdateCategoryAsync(Category category);
    Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId);
    Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate);
    Task UpdateTodoAsync(Todo todo);
    Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate);
    Task UpdateTodoImportanceAsync(Todo todo);
    bool CategoryOfSameNameExists(string name);
    IEnumerable<Category> GetCategories();
    IEnumerable<Todo> GetTodos();
    Todo? GetTodoById(Guid id);
    Category? GetCategoryById(Guid id);
}
