using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed record GetTodosForCategoryResponse(IOrderedEnumerable<Todo> TodosForCategory);
