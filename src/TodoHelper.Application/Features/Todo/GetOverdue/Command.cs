
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetOverdue;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
