
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateCategoryName;

internal sealed class UpdateCategoryNameHandler(ITodosRepository repository) : HandlerBase<UpdateCategoryNameCommand, UpdateCategoryNameResponse>(repository)
{
    public override Task<Result<UpdateCategoryNameResponse>> HandleAsync(UpdateCategoryNameCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategoryById(command.CategoryId) is not Category category)
        {
            return Task.FromResult(Result<UpdateCategoryNameResponse>.NotFoundFailure($"Category with id {command.CategoryId} not found."));
        }
        
        Result<Category> categoryResult = category.SetName(command.Name);

        // Rule: Category name must not be null, an empty string, nor all whitespace characters
        // Rule: Category name must be forty(40) characters or fewer
        if (categoryResult.IsFailure && categoryResult.Error is string error)
        {
            return Task.FromResult(Result<UpdateCategoryNameResponse>.ValidationFailure(error));
        }
        // Rule: Category name must be unique
        else if (_repository.CategoryOfSameNameExists(command.Name))
        {
            return Task.FromResult(Result<UpdateCategoryNameResponse>.DomainRuleFailure($"Category with name {command.Name} already exists."));
        }
        else if (categoryResult.IsSuccess && categoryResult.Value is Category updateCategory)
        {
            _ = _repository.UpdateCategory(updateCategory);
            return Task.FromResult(Result<UpdateCategoryNameResponse>.Success(new UpdateCategoryNameResponse(true)));
        }
        else
        {
            return Task.FromResult(Result<UpdateCategoryNameResponse>.UnknownFailure("An unknown error occurred when updating the category."));
        }
    }
}
