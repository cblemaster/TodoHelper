
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.Update;

internal sealed class Handler(IRepository<_Todo> todoRepository, IRepository<_Category> categoryRepository) : HandlerBase<_Todo, Command, Response>(todoRepository)
{
    private readonly IRepository<_Category> _categoryRepository = categoryRepository;

    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Todo? entity = await _repository.GetByIdAsync(Identifier<_Todo>.Create(command.Id));
        if (entity is null)
        {
            return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Todo))));
        }
        else if (entity.IsComplete())
        {
            return new Response(Result<bool>.Failure(Error.DomainRuleViolation("Complete todos cannot be updated.")));
        }
        else
        {
            _Category? category = await _categoryRepository.GetByIdAsync(Identifier<_Category>.Create(command.CategoryId));
            if (category is null)
            {
                return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Category))));
            }

            Result<_Todo> result = _Todo.CreateWithNew
                (
                    entity.Id,
                    category,
                    category.Id,
                    command.Description,
                    command.DueDate,
                    entity.CompleteDate,
                    command.IsImportant
                );

            if (result.IsFailure && result.Error is Error error)
            {
                return new Response(Result<bool>.Failure(Error.NotValid(error.Description)));
            }
            else if (result.IsSuccess && result.Payload is _Todo todo)
            {
                await _repository.UpdateAsync(todo);
                return new Response(Result<bool>.Success(true));
            }
            else
            {
                return new Response(Result<bool>.Failure(Error.Unknown));
            }
        }
    }
}
