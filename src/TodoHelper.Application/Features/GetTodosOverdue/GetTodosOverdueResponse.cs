
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed record GetTodosOverdueResponse(IOrderedEnumerable<Todo> OverdueTodos);
