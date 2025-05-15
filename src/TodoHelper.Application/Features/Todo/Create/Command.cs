
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.Create;

internal sealed record Command(Guid CategoryId, string Description, DateOnly? DueDate) : ICommand<Response>;
