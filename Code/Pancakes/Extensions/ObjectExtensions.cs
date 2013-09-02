// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The object extensions contains useful extensions on objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Extensions
{
	using System.Reflection;

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
	}
}