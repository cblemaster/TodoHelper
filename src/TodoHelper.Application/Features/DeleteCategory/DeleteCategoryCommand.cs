
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteCategory;

public sealed class DeleteCategoryCommand(Guid categoryId) : ICommand<DeleteCategoryResponse>
{
    public Guid CategoryId { get; } = categoryId;
}
