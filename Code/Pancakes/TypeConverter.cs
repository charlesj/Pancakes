// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The type converter is used to genericly convert an object from one type to another.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes
{
	using System;
	using System.ComponentModel;

	using Pancakes.Exceptions;

	/// <summary>
	/// The type converter is used to genericly convert an object from one type to another.
	/// </summary>
	public class TypeConverter : ITypeConverter
	{
		/// <summary>
		/// Converts an object to another type, with some error checking.
		/// </summary>
		/// <param name="value">
		/// The value is the object to convert from.
		/// </param>
		/// <param name="targetType">
		/// The target type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		/// <exception cref="PancakeException">
		/// Throws an exception if the requested conversion is not valid.
		/// </exception>
		public object Convert(object value, Type targetType)
		{
			var converter = TypeDescriptor.GetConverter(targetType);
			if (!converter.IsValid(value) || !converter.CanConvertFrom(value.GetType()))
			{
				throw new PancakeException("Attempted to convert to a type that was not valid.", new { targetType, value });
			}

			if (value is string)
			{
				return converter.ConvertFromString(value as string);
			}

			return converter.ConvertTo(value, targetType);
		}

		/// <summary>
		/// Converts an object to another type generically.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <typeparam name="TTargetType">
		/// The type to target.
		/// </typeparam>
		/// <returns>
		/// The <see cref="TTargetType"/>.
		/// </returns>
		public TTargetType Convert<TTargetType>(object value)
		{
			return (TTargetType)this.Convert(value, typeof(TTargetType));
		}
	}
}
