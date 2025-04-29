using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.DataAccess;
public interface ITodosRepository
{
    void CreateCategory(Category category);
    void CreateTodo(Todo todo);
    void DeleteCategory(Category category);
    void DeleteTodo(Todo todo);
    IOrderedEnumerable<Category> GetCategories();
    IOrderedEnumerable<Todo> GetCompleteTodos();
    IOrderedEnumerable<Todo> GetImportantTodos();
    IOrderedEnumerable<Todo> GetOverdueTodos();
    IOrderedEnumerable<Todo> GetTodosDueToday();
    void RenameCategory(Category category, string name);
    void UpdateTodoCategory(Todo todo, Identifier<Category> categoryId);
    void UpdateTodoCompleteDate(Todo todo, DateTimeOffset? completeDate);
    void UpdateTodoDescription(Todo todo, string description);
    void UpdateTodoDueDate(Todo todo, DateOnly? dueDate);
    void UpdateTodoImportance(Todo todo);
}