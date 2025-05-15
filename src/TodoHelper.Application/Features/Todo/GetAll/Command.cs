
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetAll;

internal sealed record Command() : ICommand<Response>;
