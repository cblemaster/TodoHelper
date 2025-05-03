
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosCompleted;

public class GetTodosCompletedHandler(ITodosRepository repository) : ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse>
{
    private readonly ITodosRepository _repository = repository;

    public Task<Result<GetTodosCompletedResponse>> HandleAsync(GetTodosCompletedCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos = 
            _repository.GetTodos()
                .Where(t => t.IsComplete)
                .OrderByDescending(command.FirstOrderByPredicate.Compile())
                .ThenBy(command.SecondOrderByPredicate.Compile());

        GetTodosCompletedResponse response = new(todos);
        return Task.FromResult(Result<GetTodosCompletedResponse>.Success(response));
    }
}
