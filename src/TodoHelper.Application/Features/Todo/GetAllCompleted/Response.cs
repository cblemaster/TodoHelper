
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetAllCompleted;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
