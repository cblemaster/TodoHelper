
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.CreateCategory;

public record CreateCategoryCommand(string Name) : ICommand<CreateCategoryResponse>;
