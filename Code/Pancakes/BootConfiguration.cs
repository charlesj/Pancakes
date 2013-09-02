// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootConfiguration.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The boot configuration handles the configuration for Pancakes.  It has a fluent interface that you can use to define
//   the configuration you wish, with sane defaults.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes
{
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
			this.CheckSanity = true;
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
		/// Gets or sets a value indicating whether the bootstrapper should run a check to make sure
		/// it booted correctly.  
		/// </summary>
		public bool CheckSanity { get; set; }

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
	}
}