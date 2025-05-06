
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteCategory;

internal sealed record DeleteCategoryCommand(Guid CategoryId) : ICommand<DeleteCategoryResponse>;
