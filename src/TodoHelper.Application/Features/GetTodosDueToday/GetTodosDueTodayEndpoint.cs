
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal static class GetTodosDueTodayEndpoint
{
    internal static RouteHandlerBuilder MapGetTodosDueTodayEndpoint(this WebApplication app) => app.MapGet(pattern: "/todo/duetoday",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse> handler) =>
    {
        GetTodosDueTodayCommand command = new();
        Result<GetTodosDueTodayResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.DueTodayTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
}
