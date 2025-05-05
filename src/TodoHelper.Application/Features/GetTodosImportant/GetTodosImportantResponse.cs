
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed record GetTodosImportantResponse(ICollection<TodoDTO> ImportantTodos);
