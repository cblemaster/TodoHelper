
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.SeeAllCategories;

public class SeeAllCategoriesHandler(ITodosRepository repository) : ICommandHandler<SeeAllCategoriesCommand, SeeAllCategoriesResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<SeeAllCategoriesResponse>> HandleAsync(SeeAllCategoriesCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Category> allCategories = _repository.GetCategories().OrderBy<Category, string>(command.SortPredicate.Compile());
        SeeAllCategoriesResponse response = new(allCategories);
        return Task.FromResult(Result<SeeAllCategoriesResponse>.Success(response));
    }
}
