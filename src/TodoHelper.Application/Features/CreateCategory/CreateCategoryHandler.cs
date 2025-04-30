
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateCategory;

public class CreateCategoryHandler(ITodosRepository repository) : ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;

    public async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        Result<Category> categoryResult = Category.CreateNew(command.Name);

        if (categoryResult.IsFailure && categoryResult.Error is not null)
        {
            return Result<CreateCategoryResponse>.Failure(categoryResult.Error);
        }
        else if (categoryResult.IsSuccess && categoryResult.Value is not null)
        {
            await _repository.CreateCategoryAsync(categoryResult.Value);
            return Result<CreateCategoryResponse>.Success(new CreateCategoryResponse(categoryResult.Value));    // TODO: I want the new category with id here...
        }
        else
        {
            return Result<CreateCategoryResponse>.Failure("An unknown error occurred when creating the category.");
        }
    }
}
