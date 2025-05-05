
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateCategory;

internal sealed class CreateCategoryHandler(ITodosRepository repository) : HandlerBase<CreateCategoryCommand, CreateCategoryResponse>(repository)
{
    public override async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.CategoryOfSameNameExists(command.Name))
        {
            return Result<CreateCategoryResponse>.Failure($"Category with name {command.Name} already exists.");
        }

        Result<Category> categoryResult = Category.CreateNew(command.Name);

        if (categoryResult.IsFailure && categoryResult.Error is not null)
        {
            return Result<CreateCategoryResponse>.Failure(categoryResult.Error);
        }
        else if (categoryResult.IsSuccess && categoryResult.Value is Category category)
        {
            await _repository.CreateCategoryAsync(category);
            return Result<CreateCategoryResponse>.Success
                (new CreateCategoryResponse
                    (new CategoryDTO(category.Id.Value,
                         category.Name.Value,
                         category.Todos.Count(t => !t.IsComplete),
                         category.CreateDate.Value,
                         category.UpdateDate.Value
                         )
                    )
                );
        }
        else
        {
            return Result<CreateCategoryResponse>.Failure("An unknown error occurred when creating the category.");
        }
    }
}
