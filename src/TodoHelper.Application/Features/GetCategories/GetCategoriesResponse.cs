
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed record GetCategoriesResponse(ICollection<CategoryDTO> Categories);
