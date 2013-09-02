// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsBase.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The settings base creates a basis for using ApplicationSettings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Settings
{
	using System.Configuration;

	using Pancakes.Exceptions;
	using Pancakes.Extensions;

	/// <summary>
	/// The settings base creates a basis for using ApplicationSettings.
	/// </summary>
	public class SettingsBase
	{
		/// <summary>
		/// The type converter.
		/// </summary>
		private readonly ITypeConverter typeConverter;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsBase"/> class.
		/// </summary>
		/// <param name="typeConverter">
		/// The type converter.
		/// </param>
		public SettingsBase(ITypeConverter typeConverter)
		{
			this.typeConverter = typeConverter;
		}

		/// <summary>
		/// The check all setting for values.
		/// </summary>
		public void CheckAllSettingForValues()
		{
			var properties = this.GetProperties();
			foreach (var property in properties)
			{
				var value = property.GetValue(this);
				if (value == null)
				{
					var message = string.Format("Settings property is missing value: {0}", property.Name);
					throw new PancakeException(message, this);
				}
			}
		}

		/// <summary>
		/// Gets a value from the configuration manager.
		/// </summary>
		/// <param name="key">
		/// The key in the configuration file.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		private string GetValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		/// <summary>
		/// Gets the value generically.
		/// </summary>
		/// <param name="key">
		/// The key.
		/// </param>
		/// <typeparam name="TSettingsType">
		/// The type of the settings value.
		/// </typeparam>
		/// <returns>
		/// The <see cref="TSettingsType"/>.
		/// </returns>
		private TSettingsType GetValue<TSettingsType>(string key)
		{
			return this.typeConverter.Convert<TSettingsType>(this.GetValue(key));
		}
	}
}