
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteCategory;

internal sealed class DeleteCategoryHandler(ITodosRepository repository) : HandlerBase<DeleteCategoryCommand, DeleteCategoryResponse>(repository)
{
    public async override Task<Result<DeleteCategoryResponse>> HandleAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategoryById(command.CategoryId) is not Category category)
        {
            return Result<DeleteCategoryResponse>.NotFoundFailure(DomainErrors.NotFoundErrorMessage(nameof(Category), command.CategoryId));
        }
        else
        {
            await _repository.DeleteCategoryAsync(category);
            return Result<DeleteCategoryResponse>.Success(new DeleteCategoryResponse(true));
        }
    }
}
