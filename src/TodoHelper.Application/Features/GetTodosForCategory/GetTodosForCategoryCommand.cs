
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Guid categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Guid CategoryId { get; } = categoryId;
}
