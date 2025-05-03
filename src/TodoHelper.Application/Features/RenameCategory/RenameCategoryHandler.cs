
using TodoHelper.Application.Features.DeleteCategory;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.RenameCategory;

internal sealed class RenameCategoryHandler(ITodosRepository repository) : ICommandHandler<RenameCategoryCommand, RenameCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<RenameCategoryResponse>> HandleAsync(RenameCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategories().Single(c => c.Id.Value == command.CategoryId) is not Category category)
        {
            return Task.FromResult(Result<RenameCategoryResponse>.Failure($"Category with id {command.CategoryId} not found."));
        }
        else
        {
            _ = _repository.DeleteCategoryAsync(category);
            return Task.FromResult(Result<RenameCategoryResponse>.Success(new RenameCategoryResponse(true)));
        }
    }
}
