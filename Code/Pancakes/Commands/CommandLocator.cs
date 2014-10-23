namespace Pancakes.Commands
{
	using System;

	using Pancakes;
	using Pancakes.Exceptions;

	/// <summary>
	/// The command locator.
	/// </summary>
	public class CommandLocator : ICommandLocator
	{
		/// <summary>
		/// The find command.
		/// </summary>
		/// <typeparam name="TRequest">
		/// The Type of the Request
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The Type of the Result
		/// </typeparam>
		/// <returns>
		/// The <see cref="BaseCommand"/>.
		/// </returns>
		/// <exception cref="ArchimedesException">
		/// If there is a problem, it won't solve it.
		/// </exception>
		public BaseCommand<TRequest, TResult> FindCommand<TRequest, TResult>() where TRequest : Request
		{
			try
			{
				var command = Bootstrapper.BootedKernel.ServiceLocator.GetService<BaseCommand<TRequest, TResult>>();
				return command;
			}
			catch (Exception exception)
			{
				throw new PancakeException("Could not find command.  You might not have booted, or the service locator could not find the command (missing bindings).", exception);
			}
		}
	}
}