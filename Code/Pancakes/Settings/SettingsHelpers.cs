// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsHelpers.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   Defines the SettingsHelpers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Settings
{
	using System.Configuration;

	/// <summary>
	/// The settings helpers.
	/// </summary>
	public static class SettingsHelpers
	{
		/// <summary>
		/// Gets a value from the configuration manager.
		/// </summary>
		/// <param name="key">
		/// The key.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		//public static TReturn GetValue<TReturn>(string key)
		//{
			
		//}
	}
}