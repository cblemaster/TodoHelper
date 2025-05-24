
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Get;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        _Category? entity = await _repository.GetByIdAsync(Identifier<_Category>.Create(command.Id));
        return entity is null
            ? new Response(Result<CategoryDTO>.Failure(Error.NotFound(nameof(_Category))))
            : new Response(Result<CategoryDTO>.Success(entity.MapToDTO()));
    }
}
