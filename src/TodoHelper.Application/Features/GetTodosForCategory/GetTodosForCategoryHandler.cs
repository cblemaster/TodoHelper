using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryHandler(ITodosRepository repository) : ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<GetTodosForCategoryResponse>> HandleAsync(GetTodosForCategoryCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            _repository.GetTodos()
                .Where(t => t.CategoryId.Value == command.CategoryId)
                .OrderByDescending(command.FirstOrderByPredicate.Compile())
                .ThenBy(command.SecondOrderByPredicate.Compile())
                .ThenBy(command.ThirdOrderByPredicate.Compile());

        GetTodosForCategoryResponse response = new(todos);
        return Task.FromResult(Result<GetTodosForCategoryResponse>.Success(response));
    }
}
