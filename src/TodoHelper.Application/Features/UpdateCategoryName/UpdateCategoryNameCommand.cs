
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.RenameCategory;

public sealed record UpdateCategoryNameCommand(Guid CategoryId, string Name) : ICommand<UpdateCategoryNameResponse>;
