// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Josh Charles">
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
	public class Settings : ISettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Settings"/> class.
		/// </summary>
		/// <param name="typeConverter">
		/// The type converter.
		/// </param>
		public Settings(ITypeConverter typeConverter)
		{
			this.TypeConverter = typeConverter;
			this.ApplicationName = this.GetValue("ApplicationName");
			this.ApplicationInstance = this.GetValue("ApplicationInstance");
		}

		/// <summary>
		/// Gets or sets the application name.
		/// </summary>
		public string ApplicationName { get; protected set; }

		/// <summary>
		/// Gets or sets the application instance.  Examples "Development", "Local", "Production".
		/// </summary>
		public string ApplicationInstance { get; protected set; }

		/// <summary>
		/// Gets the type converter.
		/// </summary>
		protected ITypeConverter TypeConverter { get; private set; }

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
		protected string GetValue(string key)
		{
			var value = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrEmpty(value))
			{
				string message = string.Format("Missing Settings Value: {0}", key);
				throw new PancakeException(message);
			}

			return value;
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
		protected TSettingsType GetValue<TSettingsType>(string key)
		{
			return this.TypeConverter.Convert<TSettingsType>(this.GetValue(key));
		}
	}
}