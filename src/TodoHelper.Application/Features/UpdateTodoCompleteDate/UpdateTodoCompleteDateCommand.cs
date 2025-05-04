
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoCompleted;

public sealed record UpdateTodoCompleteDateCommand(Guid TodoId, DateTimeOffset? CompleteDate) : ICommand<UpdateTodoCompleteDateResponse>;
