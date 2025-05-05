
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

internal sealed class UpdateTodoCompleteDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse>(repository)
{
    public override Task<Result<UpdateTodoCompleteDateResponse>> HandleAsync(UpdateTodoCompleteDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoCompleteDateResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            // Rule: Complete todos cannot be updated, except to update to not complete
            if (todo.IsComplete)
            {
                todo.SetNotComplete();
            }
            
            _ = _repository.UpdateTodoCompleteDateAsync(todo, command.CompleteDate);
            return Task.FromResult(Result<UpdateTodoCompleteDateResponse>.Success(new UpdateTodoCompleteDateResponse(true)));
        }
    }
}
