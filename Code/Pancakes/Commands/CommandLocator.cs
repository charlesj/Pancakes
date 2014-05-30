namespace Pancakes.Commands
{
    using System;

    using Pancakes.ServiceLocater;

    /// <summary>
    /// The command locator.
    /// </summary>
    public class CommandLocator : ICommandLocator
    {
        /// <summary>
        /// The service locater.
        /// </summary>
        private readonly IServiceLocater serviceLocater;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLocator"/> class.
        /// </summary>
        /// <param name="serviceLocater">
        /// The service locater.
        /// </param>
        public CommandLocator(IServiceLocater serviceLocater)
        {
            this.serviceLocater = serviceLocater;
        }

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
            try
            {
                return this.serviceLocater.GetService<ICommand<TRequest, TResponse>>();
            }
            catch (Exception)
            {
                throw new CommandNotFoundException(new[] { typeof(TResponse), typeof(TRequest) });
            }
        }
    }
}
