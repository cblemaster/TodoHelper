
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed class GetTodosImportantHandler(ITodosRepository repository) : HandlerBase<GetTodosImportantCommand, GetTodosImportantResponse>(repository)
{
    public override Task<Result<GetTodosImportantResponse>> HandleAsync(GetTodosImportantCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            _repository.GetTodos()
                .Where(t => t.Importance.IsImportant && !t.IsComplete)
                .OrderByDescending(command.FirstOrderByPredicate.Compile())
                .ThenBy(command.SecondOrderByPredicate.Compile());

        GetTodosImportantResponse response = new(todos);
        return Task.FromResult(Result<GetTodosImportantResponse>.Success(response));
    }
}
