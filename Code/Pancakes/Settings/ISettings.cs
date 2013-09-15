namespace Pancakes.Settings
{
	/// <summary>
	/// The Settings interface defines the basic settings that all Pancakes applications should have.
	/// </summary>
	public interface ISettings
	{
		/// <summary>
		/// Gets the application name.
		/// </summary>
		string ApplicationName { get; }

		/// <summary>
		/// Gets the application instance.  Examples "Development", "Local", "Production".
		/// </summary>
		string ApplicationInstance { get; }

		/// <summary>
		/// The check all setting for values.
		/// </summary>
		void CheckAllSettingForValues();
	}
}