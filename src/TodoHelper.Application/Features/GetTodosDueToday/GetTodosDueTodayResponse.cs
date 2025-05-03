
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed record GetTodosDueTodayResponse(IOrderedEnumerable<Todo> DueTodayTodos);
