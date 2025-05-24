
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.Create;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var repo = new TodoRepository<_Category>();

        _Category? category = await repo.GetByIdAsync(command.CategoryId);  // TODO: what if this fails?

        Result<_Todo> result = _Todo.CreateNew(category!, Identifier<_Category>.Create(command.CategoryId), command.Description, command.DueDate);

        if (result.IsFailure && result.Error is Error error)
        {
            return new Response(Result<CategoryDTO>.Failure(Error.NotValid(error.Description)));
        }
        else if (result.IsSuccess && result.Payload is _Todo todo)
        {
            todo = await _repository.CreateAsync(todo);
            return new Response(Result<TodoDTO>.Success(todo.MapToDTO()));
        }
        else
        {
            return new Response(Result<CategoryDTO>.Failure(Error.Unknown));
        }
    }
}
