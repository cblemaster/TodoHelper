
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.DeleteCategory;
using TodoHelper.Application.Features.UpdateCategoryName;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.RenameCategory;

internal sealed class UpdateCategoryNameHandler(ITodosRepository repository) : HandlerBase<UpdateCategoryNameCommand, UpdateCategoryNameResponse>(repository)
{
    public override Task<Result<UpdateCategoryNameResponse>> HandleAsync(UpdateCategoryNameCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategories().Single(c => c.Id.Value == command.CategoryId) is not Category category)
        {
            return Task.FromResult(Result<UpdateCategoryNameResponse>.Failure($"Category with id {command.CategoryId} not found."));
        }
        else
        {
            _ = _repository.UpdateCategoryNameAsync(category, command.Name);
            return Task.FromResult(Result<UpdateCategoryNameResponse>.Success(new UpdateCategoryNameResponse(true)));
        }
    }
}
