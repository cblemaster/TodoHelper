
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.SeeTodosForCategory;

internal sealed record GetTodosForCategoryResponse(IOrderedEnumerable<Todo> TodosForCategory);
