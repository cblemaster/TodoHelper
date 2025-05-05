
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

internal sealed class UpdateTodoCompleteDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse>(repository)
{
    public override Task<Result<UpdateTodoCompleteDateResponse>> HandleAsync(UpdateTodoCompleteDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoCompleteDateResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoCompleteDateAsync(todo, command.CompleteDate);
            return Task.FromResult(Result<UpdateTodoCompleteDateResponse>.Success(new UpdateTodoCompleteDateResponse(true)));
        }
    }
}
