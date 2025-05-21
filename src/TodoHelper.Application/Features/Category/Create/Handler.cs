
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Create;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        Result<_Category> result = _Category.CreateNew(command.Name);

        if (result.IsFailure && result.Error is Error error)
        {
            return Result<Response>.Failure(Error.NotValid(error.Description));
        }
        else if (result.IsSuccess && result.Payload is _Category category)
        {
            category = await _repository.CreateAsync(category);
            Response response = new(category.MapToDTO());
            return Result<Response>.Success(response);
        }
        else
        {
            return Result<Response>.Failure(Error.Unknown);
        }
    }
}
