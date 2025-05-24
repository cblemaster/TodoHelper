
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.Create;

internal sealed record Response(Result<TodoDTO> Todo);
