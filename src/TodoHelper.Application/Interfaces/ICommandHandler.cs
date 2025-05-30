﻿
namespace TodoHelper.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResponse>
{
    /// <summary>
    /// Handles the given command asynchronously and returns a response
    /// </summary>
    /// <param name="command">The command object to be handled</param>
    /// <returns>A Task representing the asynchronous operation, with a result of type TResponse</returns>
    Task<TResponse> HandleAsync(TCommand command);
}
