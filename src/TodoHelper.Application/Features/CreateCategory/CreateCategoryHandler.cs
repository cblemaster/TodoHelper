
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateCategory;

internal sealed class CreateCategoryHandler(ITodosRepository repository) : HandlerBase<CreateCategoryCommand, CreateCategoryResponse>(repository)
{
    public override async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        Result<Category> categoryResult = Category.CreateNew(command.Name);

        // Rule: Category name must not be null, an empty string, nor all whitespace characters
        // Rule: Category name must be forty(40) characters or fewer
        if (categoryResult.IsFailure && categoryResult.Error is string error)
        {
            return Result<CreateCategoryResponse>.ValidationFailure(error);
        }
        // Rule: Category name must be unique
        else if (_repository.CategoryOfSameNameExists(command.Name))
        {
            return Result<CreateCategoryResponse>.DomainRuleFailure(DomainErrors.IsNotUniqueErrorMessage(nameof(Category), nameof(Category.Name), command.Name));
        }
        else if (categoryResult.IsSuccess && categoryResult.Value is Category category)
        {
            await _repository.CreateCategoryAsync(category);
            return Result<CreateCategoryResponse>.Success(new CreateCategoryResponse (category.MapToDTO()));
        }
        else
        {
            return Result<CreateCategoryResponse>.UnknownFailure(DomainErrors.UnknownErrorMessage("creating category"));
        }
    }
}
