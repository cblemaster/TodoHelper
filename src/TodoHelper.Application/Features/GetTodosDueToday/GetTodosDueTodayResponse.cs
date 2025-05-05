
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed record GetTodosDueTodayResponse(ICollection<TodoDTO> DueTodayTodos);
