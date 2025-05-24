
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetOverdue;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
