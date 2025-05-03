
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosDueToday;

public class GetTodosDueTodayHandler(ITodosRepository repository) : ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse>
{
    private readonly ITodosRepository _repository = repository;
    
    public Task<Result<GetTodosDueTodayResponse>> HandleAsync(GetTodosDueTodayCommand command, CancellationToken cancellationToken = default)
    {
        IOrderedEnumerable<Todo> todos =
            _repository.GetTodos()
                .Where(t => t.DueDate.Value is not null && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today) && !t.IsComplete)
                .OrderBy(command.OrderByPredicate.Compile());
        GetTodosDueTodayResponse response = new(todos);
        return Task.FromResult(Result<GetTodosDueTodayResponse>.Success(response));
    }
}
