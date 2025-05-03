
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed record GetTodosImportantResponse(IOrderedEnumerable<Todo> ImportantTodos);
