
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Get;

internal class Handler(ITodosRepository<_Category> repository) : HandlerBase<Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? entity = await _repository.GetByIdAsync(command.Id);
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound);
        }
        else
        {
            Response response = new(entity.MapToDTO());
            return Result<Response>.Success(response);
        }
    }
}
