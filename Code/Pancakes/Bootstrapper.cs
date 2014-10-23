namespace Pancakes
{
	using Pancakes.Exceptions;
	using Pancakes.Mapping;

	/// <summary>
	/// The bootstrapper.
	/// </summary>
	public static class Bootstrapper
	{
		/// <summary>
		/// The booted kernel.
		/// </summary>
		private static IKernel bootedKernel;

		/// <summary>
		/// Gets a value indicating whether the kernel has booted.
		/// </summary>
		public static bool HasBooted
		{
			get
			{
				return bootedKernel == null;
			}
		}

		/// <summary>
		/// Gets the booted kernel.  Only accessible after Boot() is called.
		/// </summary>
		public static IKernel BootedKernel
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
				MappingConfigurationLoader.LoadConfigurations();
			}
		}
	}
}