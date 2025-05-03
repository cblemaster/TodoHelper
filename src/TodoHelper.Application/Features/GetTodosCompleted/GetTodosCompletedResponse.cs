
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosCompleted;

public record GetTodosCompletedResponse(IOrderedEnumerable<Todo> CompleteTodos);
