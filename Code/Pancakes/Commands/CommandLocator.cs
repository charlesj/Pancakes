namespace Pancakes.Commands
{
    /// <summary>
    /// The command locator.
    /// </summary>
    public class CommandLocator : ICommandLocator
    {
        /// <summary>
        /// The locate command.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of the request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The type of the response.
        /// </typeparam>
        /// <returns>
        /// The <see cref="ICommand"/>.
        /// </returns>
        public ICommand<TRequest, TResponse> LocateCommand<TRequest, TResponse>()
        {
            throw new CommandNotFoundException(new[] { typeof(TResponse), typeof(TRequest) });
        }
    }
}
