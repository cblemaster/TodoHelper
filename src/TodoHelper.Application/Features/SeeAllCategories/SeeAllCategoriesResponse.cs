
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.SeeAllCategories;

public record SeeAllCategoriesResponse(IOrderedEnumerable<Category> AllCategories);
