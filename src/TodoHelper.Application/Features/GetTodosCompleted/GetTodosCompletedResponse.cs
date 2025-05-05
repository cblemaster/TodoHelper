
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed record GetTodosCompletedResponse(ICollection<TodoDTO> CompleteTodos);
