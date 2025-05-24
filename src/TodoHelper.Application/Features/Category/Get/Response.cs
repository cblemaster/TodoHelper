
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Category.Get;

internal sealed record Response(Result<CategoryDTO> Category);
