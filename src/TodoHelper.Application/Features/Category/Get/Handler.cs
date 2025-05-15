
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Get;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? entity = await _repository.GetByIdAsync(command.Id);
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound(nameof(_Category)));
        }
        else
        {
            Response response = new(entity.MapToDTO());
            return Result<Response>.Success(response);
        }
    }
}
