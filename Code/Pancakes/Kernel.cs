namespace Pancakes
{
	using System;

	using Pancakes.ServiceLocater;

	/// <summary>
	/// The bootstrapper is the entry point for applications written with Pancakes.  Calling Boot() with the boot configuration gets everything 
	/// configured and ready to go.
	/// </summary>
	public class Kernel : IKernel
	{
		/// <summary>
		/// The configuration that was set at startup.
		/// </summary>
		private readonly BootConfiguration configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="Kernel"/> class.
		/// </summary>
		/// <param name="configuration">
		/// The configuration the kernel should run.
		/// </param>
		public Kernel(BootConfiguration configuration)
		{
			this.configuration = configuration;
			this.WriteIfVerbose("Booting...");

			this.ServiceLocator = new NinjectServiceLocator();

			if (this.configuration.CheckSanity)
			{
				this.CheckSanity();
			}
			
			this.WriteIfVerbose("Boot Complete.");
		}

		/// <summary>
		/// Gets the service locater.
		/// </summary>
		public IServiceLocater ServiceLocator { get; private set; }

		/// <summary>
		/// Conducts a sanity check on the booted result.
		/// </summary>
		public void CheckSanity()
		{
			this.WriteIfVerbose("Checking Sanity");
			//this.CheckSanityOfSettingsObjects();
			this.WriteIfVerbose("Hey it's sane!");
		}

		/// <summary>
		/// The write if verbose.
		/// </summary>
		/// <param name="format">
		/// The format.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		public void WriteIfVerbose(string format, params object[] args)
		{
			if (this.configuration.Verbose)
			{
				Console.WriteLine(format, args);
			}
		}
	}
}
