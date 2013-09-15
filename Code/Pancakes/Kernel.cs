namespace Pancakes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Pancakes.Exceptions;
	using Pancakes.ServiceLocater;
	using Pancakes.Settings;

	/// <summary>
	/// The bootstrapper is the entry point for applications written with Pancakes.  Calling Boot() with the boot configuration gets everything 
	/// configured and ready to go.
	/// </summary>
	public class Kernel
	{
		/// <summary>
		/// The booted kernel available after boot() is called.
		/// </summary>
		private static Kernel bootedKernel;

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

			this.ServiceLocater = new NinjectServiceLocater();

			if (this.configuration.CheckSanity)
			{
				this.CheckSanity();
			}
			
			this.WriteIfVerbose("Boot Complete.");
		}

		/// <summary>
		/// Gets the booted kernel.  Only accessible after Boot() is called.
		/// </summary>
		public static Kernel BootedKernel
		{
			get
			{
				if (bootedKernel == null)
				{
					throw new PancakeException("You cannot access the pancake kernel without first booting it.");
				}

				return bootedKernel;
			}

			private set
			{
				bootedKernel = value;
			}
		}

		/// <summary>
		/// Gets the service locater.
		/// </summary>
		public IServiceLocater ServiceLocater { get; private set; }
	
		/// <summary>
		/// Boots up the stack.
		/// </summary>
		/// <param name="configuration">
		/// The configuration.
		/// </param>
		public static void Boot(BootConfiguration configuration)
		{
			if (bootedKernel == null)
			{
				var kernel = new Kernel(configuration);
				BootedKernel = kernel;
			}
		}

		/// <summary>
		/// Conducts a sanity check on the booted result.
		/// </summary>
		public void CheckSanity()
		{
			this.WriteIfVerbose("Checking Sanity");
			this.CheckSanityOfSettingsObjects();
			this.WriteIfVerbose("Hey it's sane!");
		}

		/// <summary>
		/// Checks the sanity of Settings objects by loading them via the service locater and calling CheckAllSettingForValues().
		/// </summary>
		private void CheckSanityOfSettingsObjects()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var settingsInterfaces = new List<Type>();
			foreach (var assembly in assemblies)
			{
				settingsInterfaces.AddRange(assembly.GetTypes().Where(type => type.IsInterface && type.IsAssignableFrom(typeof(ISettings))));
			}

			foreach (var settingsInterface in settingsInterfaces)
			{
				var settings = (ISettings)this.ServiceLocater.GetService(settingsInterface);
				settings.CheckAllSettingForValues();
			}

			this.WriteIfVerbose("Settings implementations seem to be sane.");
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
		private void WriteIfVerbose(string format, params object[] args)
		{
			if (this.configuration.Verbose)
			{
				Console.WriteLine(format, args);
			}
		}
	}
}
