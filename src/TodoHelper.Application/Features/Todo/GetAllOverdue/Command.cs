
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetAllOverdue;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
