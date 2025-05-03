
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.CreateCategory;

internal sealed record CreateCategoryCommand(string Name) : ICommand<CreateCategoryResponse>;
