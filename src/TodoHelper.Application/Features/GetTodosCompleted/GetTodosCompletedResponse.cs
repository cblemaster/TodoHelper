
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed record GetTodosCompletedResponse(IOrderedEnumerable<Todo> CompleteTodos);
