
using TodoHelper.Application.DataTransferObjects;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed record GetTodosForCategoryResponse(ICollection<TodoDTO> TodosForCategory);
