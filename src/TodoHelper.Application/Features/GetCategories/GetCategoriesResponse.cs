
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed record GetCategoriesResponse(IOrderedEnumerable<Category> Categories);
