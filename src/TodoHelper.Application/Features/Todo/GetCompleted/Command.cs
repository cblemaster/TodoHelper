
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetCompleted;

internal sealed record Command() : ICommand<Response>;
