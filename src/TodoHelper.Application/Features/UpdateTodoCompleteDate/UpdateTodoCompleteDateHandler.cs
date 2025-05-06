
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

internal sealed class UpdateTodoCompleteDateHandler(ITodosRepository repository) : HandlerBase<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse>(repository)
{
    public async override Task<Result<UpdateTodoCompleteDateResponse>> HandleAsync(UpdateTodoCompleteDateCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<UpdateTodoCompleteDateResponse>.NotFoundFailure(DomainErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        else
        {
            // Rule: Complete todos cannot be updated, except to update to not complete
            if (todo.IsComplete)
            {
                todo.SetNotComplete();
                await _repository.UpdateTodoAsync(todo);
            }
            else
            {
                await _repository.UpdateTodoCompleteDateAsync(todo, command.CompleteDate);
            }

            return Result<UpdateTodoCompleteDateResponse>.Success(new UpdateTodoCompleteDateResponse(true));
        }
    }
}
