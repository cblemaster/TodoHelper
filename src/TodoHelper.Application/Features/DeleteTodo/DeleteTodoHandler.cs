
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteTodo;

public sealed class DeleteTodoHandler(ITodosRepository repository) : HandlerBase<DeleteTodoCommand, DeleteTodoResponse>(repository)
{
    public override Task<Result<DeleteTodoResponse>> HandleAsync(DeleteTodoCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<DeleteTodoResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.DeleteTodoAsync(todo);
            return Task.FromResult(Result<DeleteTodoResponse>.Success(new DeleteTodoResponse(true)));
        }
    }
}
