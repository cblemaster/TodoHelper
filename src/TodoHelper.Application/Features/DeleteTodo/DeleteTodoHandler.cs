
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.Specifications;

namespace TodoHelper.Application.Features.DeleteTodo;

internal sealed class DeleteTodoHandler(ITodosRepository repository) : HandlerBase<DeleteTodoCommand, DeleteTodoResponse>(repository)
{
    public async override Task<Result<DeleteTodoResponse>> HandleAsync(DeleteTodoCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Result<DeleteTodoResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Todo), command.TodoId));
        }
        // Rule: Important todos cannot be deleted
        else if (todo.CanBeDeleted)
        {
            return Result<DeleteTodoResponse>.DomainRuleFailure(DomainErrors.CannotDeleteImportantTodosErrorMessage());
        }
        else
        {
            await _repository.DeleteTodoAsync(todo);
            return Result<DeleteTodoResponse>.Success(new DeleteTodoResponse(true));
        }
    }
}
