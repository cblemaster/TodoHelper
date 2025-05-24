
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetCompleted;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
