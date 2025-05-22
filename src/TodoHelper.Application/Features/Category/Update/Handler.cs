
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Update;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? entity = await _repository.GetByIdAsync(Identifier<_Category>.Create(command.Id));
        if (entity is null)
        {
            return Result<Response>.Failure(Error.NotFound(nameof(_Category)));
        }
        else
        {
            //Result<_Category> result = entity.Update(command.Name);
            Result<_Category> result = Result<_Category>.Failure(Error.NotValid(string.Empty));

            _repository.DisposeEntity(entity);

            if (result.IsFailure && result.Error is Error error)
            {
                return Result<Response>.Failure(Error.NotValid(error.Description));
            }
            else if (result.IsSuccess && result.Payload is _Category category)
            {
                await _repository.UpdateAsync(category);
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
