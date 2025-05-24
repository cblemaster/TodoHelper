
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Category.GetAll;

internal sealed record Response(Result<IEnumerable<CategoryDTO>> Categories);
