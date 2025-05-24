
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.BaseClasses;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Todo.GetAllByCategory;

internal sealed record Command(Identifier<_Category> CategoryId, bool IncludeComplete) : ICommand<Response>;
