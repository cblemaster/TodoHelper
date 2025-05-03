
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteCategory;

internal class DeleteCategoryHandler(ITodosRepository repository) : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<DeleteCategoryResponse>> HandleAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        if (_repository.GetCategories().Single(c => c.Id.Value == command.CategoryId) is not Category category)
        {
            return Task.FromResult(Result<DeleteCategoryResponse>.Failure($"Category with id {command.CategoryId} not found."));
        }
        else
        {
            _repository.DeleteCategoryAsync(category);
            return Task.FromResult(Result<DeleteCategoryResponse>.Success(new DeleteCategoryResponse(true)));
        }
    }
}
