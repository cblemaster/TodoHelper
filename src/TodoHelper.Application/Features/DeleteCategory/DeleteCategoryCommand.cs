
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid CategoryId) : ICommand<DeleteCategoryResponse>;
