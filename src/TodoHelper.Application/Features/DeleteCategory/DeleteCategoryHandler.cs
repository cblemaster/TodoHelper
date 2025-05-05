
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteCategory;

internal sealed class DeleteCategoryHandler(ITodosRepository repository) : HandlerBase<DeleteCategoryCommand, DeleteCategoryResponse>(repository)
{
    public override Task<Result<DeleteCategoryResponse>> HandleAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategoryById(command.CategoryId) is not Category category)
        {
            return Task.FromResult(Result<DeleteCategoryResponse>.NotFoundFailure($"Category with id {command.CategoryId} not found."));
        }
        else
        {
            _ = _repository.DeleteCategoryAsync(category);
            return Task.FromResult(Result<DeleteCategoryResponse>.Success(new DeleteCategoryResponse(true)));
        }
    }
}
