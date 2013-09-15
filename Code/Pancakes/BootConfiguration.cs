namespace Pancakes
{
	using System.Collections.Generic;

	/// <summary>
	/// The boot configuration handles the configuration for Pancakes.  It has a fluent interface that you can use to define
	/// the configuration you wish, with sane defaults.
	/// </summary>
	public class BootConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BootConfiguration"/> class.
		/// </summary>
		public BootConfiguration()
		{
			this.AssemblySearchPatterns = new List<string>();
			this.CheckSanity = true;
			this.Verbose = false;
		}

		/// <summary>
		/// Gets the default configuration.
		/// </summary>
		public static BootConfiguration DefaultConfiguration
		{
			get
			{
				return new BootConfiguration();
			}
		}

		/// <summary>
		/// Gets or sets the assembly search patterns.
		/// </summary>
		public List<string> AssemblySearchPatterns { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the bootstrapper should run a check to make sure
		/// it booted correctly.  
		/// </summary>
		public bool CheckSanity { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether verbose, which means messages will be written to Standard Out.
		/// </summary>
		public bool Verbose { get; set; }

		/// <summary>
		/// Sets the configuration to skip the sanity check at the end. 
		/// </summary>
		/// <returns>
		/// The <see cref="BootConfiguration"/>.
		/// </returns>
		public BootConfiguration SkipSanityCheck()
		{
			this.CheckSanity = false;
			return this;
		}

		/// <summary>
		/// The add assembly search pattern.
		/// </summary>
		/// <param name="searchPattern">
		/// The search pattern.
		/// </param>
		/// <returns>
		/// The <see cref="BootConfiguration"/>.
		/// </returns>
		public BootConfiguration AddAssemblySearchPattern(string searchPattern)
		{
			this.AssemblySearchPatterns.Add(searchPattern);
			return this;
		}

		/// <summary>
		/// With this set, the boot process will write messages out to the console.
		/// </summary>
		/// <returns>
		/// The Bootconfiguration with Verbosity on.
		/// </returns>
		public BootConfiguration BeVerbose()
		{
			this.Verbose = true;
			return this;
		}
	}
}