
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.UpdateCompleteDate;

public sealed record Command(Guid Id, DateTimeOffset? CompleteDate) : ICommand<Response>;
