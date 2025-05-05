
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed record GetTodosOverdueResponse(ICollection<TodoDTO> OverdueTodos);
