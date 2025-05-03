
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.RenameCategory;

public sealed class RenameCategoryCommand(Guid categoryId, string name) : ICommand<RenameCategoryResponse>
{
    public Guid CategoryId { get; } = categoryId;
    public string Name { get; } = name;
}
