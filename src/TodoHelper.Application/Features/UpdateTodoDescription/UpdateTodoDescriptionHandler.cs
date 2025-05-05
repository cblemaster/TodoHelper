
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

internal sealed class UpdateTodoDescriptionHandler(ITodosRepository repository) : HandlerBase<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse>(repository)
{
    public override Task<Result<UpdateTodoDescriptionResponse>> HandleAsync(UpdateTodoDescriptionCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetTodos().Single(t => t.Id.Value == command.TodoId) is not Todo todo)
        {
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.Failure($"Todo with id {command.TodoId} not found."));
        }
        else
        {
            _ = _repository.UpdateTodoDescriptionAsync(todo, command.Name);
            return Task.FromResult(Result<UpdateTodoDescriptionResponse>.Success(new UpdateTodoDescriptionResponse(true)));
        }
    }
}
