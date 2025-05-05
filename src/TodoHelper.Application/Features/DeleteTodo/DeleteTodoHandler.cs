
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteTodo;

public sealed class DeleteTodoHandler(ITodosRepository repository) : HandlerBase<DeleteTodoCommand, DeleteTodoResponse>(repository)
{
    public override Task<Result<DeleteTodoResponse>> HandleAsync(DeleteTodoCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodoById(command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<DeleteTodoResponse>.NotFoundFailure($"Todo with id {command.TodoId} not found."));
        }
        // Rule: Important todos cannot be deleted
        else if (todo.CanBeDeleted)
        {
            return Task.FromResult(Result<DeleteTodoResponse>.DomainRuleFailure($"Important todos cannot be deleted."));
        }
        else
        {
            _ = _repository.DeleteTodoAsync(todo);
            return Task.FromResult(Result<DeleteTodoResponse>.Success(new DeleteTodoResponse(true)));
        }
    }
}
