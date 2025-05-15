
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.Delete;

internal sealed record Command(Guid Id) : ICommand<Response>;
