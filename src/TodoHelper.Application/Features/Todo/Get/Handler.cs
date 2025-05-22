
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
namespace TodoHelper.Application.Features.Todo.Get;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Todo? entity = await _repository.GetByIdAsync(Identifier<_Todo>.Create(command.Id));
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound(nameof(_Todo)));
        }
        else
        {
            Response response = new(entity.MapToDTO());
            return Result<Response>.Success(response);
        }
    }
}
