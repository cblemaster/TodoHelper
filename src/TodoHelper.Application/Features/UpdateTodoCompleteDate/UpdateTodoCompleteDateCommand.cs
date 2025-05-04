
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

public sealed record UpdateTodoCompleteDateCommand(Guid TodoId, DateTimeOffset? CompleteDate) : ICommand<UpdateTodoCompleteDateResponse>;
