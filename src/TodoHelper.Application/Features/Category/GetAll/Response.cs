
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.Category.GetAll;

internal sealed record Response(IEnumerable<CategoryDTO> Categories);
