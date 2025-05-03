
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosOverdue;

public record GetTodosOverdueResponse(IOrderedEnumerable<Todo> OverdueTodos);
