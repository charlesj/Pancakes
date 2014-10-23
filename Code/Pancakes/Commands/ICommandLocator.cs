namespace Pancakes.Commands
{
	/// <summary>
	/// The CommandLocator interface.
	/// </summary>
	public interface ICommandLocator
	{
		/// <summary>
		/// The find command.
		/// </summary>
		/// <typeparam name="TRequest">
		/// The type of the request.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the result.
		/// </typeparam>
		/// <returns>
		/// The <see cref="BaseCommand"/>.
		/// </returns>
		BaseCommand<TRequest, TResult> FindCommand<TRequest, TResult>() where TRequest : Request;
	}
}