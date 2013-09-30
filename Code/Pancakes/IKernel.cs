namespace Pancakes
{
	using Pancakes.ServiceLocater;

	/// <summary>
	/// The Kernel interface.
	/// </summary>
	public interface IKernel
	{
		/// <summary>
		/// Gets the service locater.
		/// </summary>
		IServiceLocater ServiceLocater { get; }

		/// <summary>
		/// Conducts a sanity check on the booted result.
		/// </summary>
		void CheckSanity();

		/// <summary>
		/// The write if verbose.
		/// </summary>
		/// <param name="format">
		/// The format.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		void WriteIfVerbose(string format, params object[] args);
	}
}