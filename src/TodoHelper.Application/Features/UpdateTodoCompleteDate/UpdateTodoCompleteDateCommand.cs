
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

internal sealed record UpdateTodoCompleteDateCommand(Guid TodoId, DateTimeOffset? CompleteDate) : ICommand<UpdateTodoCompleteDateResponse>;
