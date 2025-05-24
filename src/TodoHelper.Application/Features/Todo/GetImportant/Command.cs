
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetImportant;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
