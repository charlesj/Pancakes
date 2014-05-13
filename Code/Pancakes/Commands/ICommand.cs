namespace Pancakes.Commands
{
    /// <summary>
    /// The Command interface.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response.
    /// </typeparam>
    public interface ICommand<in TRequest, out TResponse>
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="TResponse"/>.
        /// </returns>
        TResponse Execute(TRequest request);
    }
}