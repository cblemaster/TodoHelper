
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetCategories;

public class GetCategoriesHandler(ITodosRepository repository) : ICommandHandler<GetCategoriesCommand, GetCategoriesResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<GetCategoriesResponse>> HandleAsync(GetCategoriesCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Category> categories = _repository.GetCategories().OrderBy(command.OrderByPredicate.Compile());
        GetCategoriesResponse response = new(categories);
        return Task.FromResult(Result<GetCategoriesResponse>.Success(response));
    }
}
