
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateCategoryName;

public sealed record UpdateCategoryNameCommand(Guid CategoryId, string Name) : ICommand<UpdateCategoryNameResponse>;
