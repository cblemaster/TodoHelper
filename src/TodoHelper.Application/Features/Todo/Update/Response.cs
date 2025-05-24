
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.Update;

internal sealed record Response(Result<bool> Result);
