
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.Update;

internal sealed record Command(Guid Id, Guid CategoryId, string Description, DateOnly? DueDate, DateTimeOffset? CompleteDate, bool IsImportant) : ICommand<Response>;
