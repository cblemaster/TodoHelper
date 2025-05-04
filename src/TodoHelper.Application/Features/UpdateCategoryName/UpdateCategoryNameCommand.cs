
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.RenameCategory;

public sealed class UpdateCategoryNameCommand(Guid categoryId, string name) : ICommand<UpdateCategoryNameResponse>
{
    public Guid CategoryId { get; } = categoryId;
    public string Name { get; } = name;
}
