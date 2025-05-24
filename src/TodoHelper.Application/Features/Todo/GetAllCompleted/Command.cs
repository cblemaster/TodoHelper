
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetAllCompleted;

internal sealed record Command() : ICommand<Response>;
