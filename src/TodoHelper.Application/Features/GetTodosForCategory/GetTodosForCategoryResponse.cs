
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.SeeTodosForCategory;

public record GetTodosForCategoryResponse(IOrderedEnumerable<Todo> TodosForCategory);
