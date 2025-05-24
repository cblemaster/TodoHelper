
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;

namespace TodoHelper.Application.Features.Todo.Delete;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Todo? entity = await _repository.GetByIdAsync(Identifier<_Todo>.Create(command.Id));
        if (entity is null)
        {
            return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Todo))));
        }
        else if (entity.IsImportant())
        {
            return new Response(Result<bool>.Failure(Error.DomainRuleViolation("Important todos cannot be deleted.")));
        }
        else
        {
            await _repository.DeleteAsync(entity);
            return new Response(Result<bool>.Success(true));
        }
    }
}
