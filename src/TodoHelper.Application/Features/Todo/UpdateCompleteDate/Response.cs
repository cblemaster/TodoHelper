
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.UpdateCompleteDate;

internal sealed record Response(Result<bool> Result);
