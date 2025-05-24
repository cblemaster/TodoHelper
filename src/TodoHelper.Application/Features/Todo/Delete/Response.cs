
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.Delete;

internal sealed record Response(Result<bool> Result);
