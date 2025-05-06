
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateCategoryName;

internal sealed record UpdateCategoryNameCommand(Guid CategoryId, string Name) : ICommand<UpdateCategoryNameResponse>;
