
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetAllByCategory;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
