
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetCategories;

internal sealed class GetCategoriesHandler(ITodosRepository repository) : HandlerBase<GetCategoriesCommand, GetCategoriesResponse>(repository)
{
    public override Task<Result<GetCategoriesResponse>> HandleAsync(GetCategoriesCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Category> categories = _repository.GetCategories().OrderBy(command.OrderByPredicate.Compile());
        GetCategoriesResponse response = new(categories);
        return Task.FromResult(Result<GetCategoriesResponse>.Success(response));
    }
}
