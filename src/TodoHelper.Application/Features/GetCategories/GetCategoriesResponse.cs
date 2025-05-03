
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetCategories;

public record GetCategoriesResponse(IOrderedEnumerable<Category> Categories);
