
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.Category.GetAll;

internal record Response(IEnumerable<CategoryDTO> Categories);
