
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetAllImportant;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
