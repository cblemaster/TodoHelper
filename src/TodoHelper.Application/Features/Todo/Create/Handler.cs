
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
using _Category = TodoHelper.Domain.Entities.Category;
using TodoHelper.Domain.BaseClasses;
namespace TodoHelper.Application.Features.Todo.Create;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? category = _Category.CreateNew("").Payload;

        Result<_Todo> result = _Todo.CreateNew(category!, Identifier<_Category>.Create(command.CategoryId), command.Description, command.DueDate);

        if (result.IsFailure && result.Error is Error error)
        {
            return Result<Response>.Failure(Error.NotValid(error.Description));
        }
        else if (result.IsSuccess && result.Payload is _Todo todo)
        {
            todo = await _repository.CreateAsync(todo);
            Response response = new(todo.MapToDTO());
            return Result<Response>.Success(response);
        }
        else
        {
            return Result<Response>.Failure(Error.Unknown);
        }
    }
}
