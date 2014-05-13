namespace Pancakes.Commands
{
    /// <summary>
    /// The CommandExecutor interface.
    /// </summary>
    public interface ICommandLocator
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of the request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The type of the response.
        /// </typeparam>
        /// <returns>
        /// The command that matches the requirements.
        /// </returns>
        ICommand<TRequest, TResponse> LocateCommand<TRequest, TResponse>();
    }
}