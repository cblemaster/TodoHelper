
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

internal sealed class Handler(IRepository<_Todo> todoRepository, IRepository<_Category> categoryRepository) : HandlerBase<_Todo, Command, Response>(todoRepository)
{
    private readonly IRepository<_Category> _categoryRepository = categoryRepository;

    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? category = await _categoryRepository.GetByIdAsync(Identifier<_Category>.Create(command.CategoryId));
        if (category is null)
        {
            return new Response(Result<TodoDTO>.Failure(Error.NotFound(nameof(_Category))));
        }

        Result<_Todo> result = _Todo.CreateNew(category!, Identifier<_Category>.Create(command.CategoryId), command.Description, command.DueDate);

        if (result.IsFailure && result.Error is Error error)
        {
            return new Response(Result<TodoDTO>.Failure(Error.NotValid(error.Description)));
        }
        else if (result.IsSuccess && result.Payload is _Todo todo)
        {
            todo = await _repository.CreateAsync(todo);
            return new Response(Result<TodoDTO>.Success(todo.MapToDTO()));
        }
        else
        {
            return new Response(Result<TodoDTO>.Failure(Error.Unknown));
        }
    }
}
