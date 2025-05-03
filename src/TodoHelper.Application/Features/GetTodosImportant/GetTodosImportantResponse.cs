
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

public record GetTodosImportantResponse(IOrderedEnumerable<Todo> ImportantTodos);
