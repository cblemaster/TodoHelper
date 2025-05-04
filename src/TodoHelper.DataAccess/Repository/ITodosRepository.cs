
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess.Repository;
public interface ITodosRepository
{
    Task CreateCategoryAsync(Category category);
    Task CreateTodoAsync(Todo todo);
    Task DeleteCategoryAsync(Category category);
    Task DeleteTodoAsync(Todo todo);
    Task UpdateCategoryNameAsync(Category category, string name);
    Task UpdateTodoCategoryAsync(Todo todo, Identifier<Category> categoryId);
    Task UpdateTodoCompleteDateAsync(Todo todo, DateTimeOffset? completeDate);
    Task UpdateTodoDescriptionAsync(Todo todo, string description);
    Task UpdateTodoDueDateAsync(Todo todo, DateOnly? dueDate);
    Task UpdateTodoImportanceAsync(Todo todo);
    bool CategoryOfSameNameExists(string name);
    Task<Category> GetCategoryByIdAsync(Identifier<Category> categoryId);
    IEnumerable<Category> GetCategories();
    Task<Todo> GetTodoByIdAsync(Identifier<Todo> todoId);
    IEnumerable<Todo> GetTodosByCategoryId(Identifier<Category> categoryId);
    IEnumerable<Todo> GetTodos();
}
