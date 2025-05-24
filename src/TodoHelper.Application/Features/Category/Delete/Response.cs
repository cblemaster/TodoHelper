
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Category.Delete;

internal sealed record Response(Result<bool> Result);
