namespace Pancakes.Extensions
{
	using System.Reflection;

	using Newtonsoft.Json;

	/// <summary>
	/// The object extensions contains useful extensions on objects.
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// Gets all the properties on the object.
		/// </summary>
		/// <param name="obj">
		/// The obj.
		/// </param>
		/// <returns>
		/// An array of all the properties.
		/// </returns>
		public static PropertyInfo[] GetProperties(this object obj)
		{
			return obj.GetType().GetProperties();
		}

		/// <summary>
		/// Serializes the object into json.
		/// </summary>
		/// <param name="obj">
		/// The obj.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string ToJson(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}