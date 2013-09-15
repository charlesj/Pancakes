namespace Pancakes
{
	using System;

	/// <summary>
	/// The TypeConvertor interface defines a generic way to do type conversion.
	/// </summary>
	public interface ITypeConverter
	{
		/// <summary>
		/// Converts the object from one type to another type.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <param name="targetType">
		/// The target type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object Convert(object value, Type targetType);

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
		TTargetType Convert<TTargetType>(object value);
	}
}