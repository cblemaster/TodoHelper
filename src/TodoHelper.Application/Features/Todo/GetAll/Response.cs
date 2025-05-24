
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetAll;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
