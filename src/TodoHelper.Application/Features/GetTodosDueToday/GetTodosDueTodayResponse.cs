
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosDueToday;

public record GetTodosDueTodayResponse(IOrderedEnumerable<Todo> DueTodayTodos);
