
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateCategory;

internal sealed class CreateCategoryHandler(ITodosRepository repository) : ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;

    public async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
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
        else if (categoryResult.IsSuccess && categoryResult.Value is not null)
        {
            await _repository.CreateCategoryAsync(categoryResult.Value);
            return Result<CreateCategoryResponse>.Success(new CreateCategoryResponse(categoryResult.Value));
        }
        else
        {
            return Result<CreateCategoryResponse>.Failure("An unknown error occurred when creating the category.");
        }
    }
}
