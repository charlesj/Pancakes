namespace Pancakes.Settings
{
	using System;

	using Pancakes.Exceptions;
	using Pancakes.Extensions;

	/// <summary>
	/// This settings object uses reflection to load the settings from application configuration.
	/// </summary>
	public class ReflectiveSettings : Settings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReflectiveSettings"/> class.
		/// </summary>
		/// <param name="typeConverter">
		/// The type converter.
		/// </param>
		public ReflectiveSettings(ITypeConverter typeConverter) : base(typeConverter)
		{
			this.LoadSettingsUsingReflection();
		}

		/// <summary>
		/// Sets the values of the properties of this object with the values from the application configuration file.
		/// </summary>
		protected void LoadSettingsUsingReflection()
		{
			var properties = this.GetProperties();
			foreach (var property in properties)
			{
				try
				{
					var settingsValue = this.GetValue(property.Name);
					var propertyType = property.PropertyType;
					var typedValue = this.TypeConverter.Convert(settingsValue, propertyType);
					property.SetValue(this, typedValue);
				}
				catch (Exception exception)
				{
					string message = string.Format("Error trying to set a value for {0} from application configuration.  See inner exception for more details.", property.Name);
					throw new PancakeException(message, exception);
				}
			}
		}
	}
}