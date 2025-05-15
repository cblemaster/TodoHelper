
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.Get;

internal sealed record Command(Guid Id) : ICommand<Response>;
