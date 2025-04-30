
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResponse>
{
    /// <summary>
    /// Handles the given command asynchronously and returns a response
    /// </summary>
    /// <param name="command">The command object to be handled</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests (optional)</param>
    /// <returns>A Task representing the asynchronous operation, with a result of type TResponse</returns>
    Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
