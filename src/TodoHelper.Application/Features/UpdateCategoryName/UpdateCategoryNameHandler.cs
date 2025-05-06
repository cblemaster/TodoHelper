
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.Specifications;

namespace TodoHelper.Application.Features.UpdateCategoryName;

internal sealed class UpdateCategoryNameHandler(ITodosRepository repository) : HandlerBase<UpdateCategoryNameCommand, UpdateCategoryNameResponse>(repository)
{
    public async override Task<Result<UpdateCategoryNameResponse>> HandleAsync(UpdateCategoryNameCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategoryById(command.CategoryId) is not Category category)
        {
            return Result<UpdateCategoryNameResponse>.NotFoundFailure(ApplicationErrors.NotFoundErrorMessage(nameof(Category), command.CategoryId));
        }

        Result<Category> categoryResult = category.SetName(command.Name);

        // Rule: Category name must not be null, an empty string, nor all whitespace characters
        // Rule: Category name must be forty(40) characters or fewer
        if (categoryResult.IsFailure && categoryResult.Error is string error)
        {
            return Result<UpdateCategoryNameResponse>.ValidationFailure(error);
        }
        // Rule: Category name must be unique
        else if (_repository.CategoryOfSameNameExists(command.Name))
        {
            return Result<UpdateCategoryNameResponse>.DomainRuleFailure(DomainErrors.IsNotUniqueErrorMessage(nameof(Category), nameof(Category.Name), command.Name));
        }
        else if (categoryResult.IsSuccess && categoryResult.Value is Category updateCategory)
        {
            await _repository.UpdateCategoryAsync(updateCategory);
            return Result<UpdateCategoryNameResponse>.Success(new UpdateCategoryNameResponse(true));
        }
        else
        {
            return Result<UpdateCategoryNameResponse>.UnknownFailure(DomainErrors.UnknownErrorMessage("updating the category"));
        }
    }
}
