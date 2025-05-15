
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.Todo.GetAll;

internal sealed record Response(IEnumerable<TodoDTO> Todos);
