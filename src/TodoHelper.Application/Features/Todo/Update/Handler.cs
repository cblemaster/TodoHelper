
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.Update;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Todo? entity = await _repository.GetByIdAsync(command.Id);
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound(nameof(_Todo)));
        }
        else
        {
            Result<_Todo> result = entity.Update(command.Id, command.CategoryId,
                command.Description, command.DueDate, command.CompleteDate, command.IsImportant);

            _repository.DisposeEntity(entity);

            if (result.IsFailure && result.Error is Error error)
            {
                return Result<Response>.Failure(Error.NotValid(error.Description));
            }
            else if (result.IsSuccess && result.Payload is _Todo todo)
            {
                await _repository.UpdateAsync(todo);
                Response response = new();
                return Result<Response>.Success(response);
            }
            else
            {
                return Result<Response>.Failure(Error.Unknown);
            }
        }
    }
}
