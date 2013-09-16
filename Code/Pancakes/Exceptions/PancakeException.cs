namespace Pancakes.Exceptions
{
	using System;

	/// <summary>
	/// The pancake exception provides a base for working with exception inside of Pancakes.
	/// </summary>
	public class PancakeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PancakeException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="innerException">
		/// The inner exception.
		/// </param>
		/// <param name="additionalData">
		/// The additional data.
		/// </param>
		public PancakeException(string message, Exception innerException, object additionalData = null) : base(message, innerException)
		{
			this.AdditionalData = additionalData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PancakeException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message of the exception.
		/// </param>
		/// <param name="additionalData">
		/// Additional data can be used to wrap up any relevant information that you wish to include.
		/// </param>
		public PancakeException(string message, object additionalData = null) : base(message)
		{
			this.AdditionalData = additionalData;
		}

		/// <summary>
		/// Prevents a default instance of the <see cref="PancakeException"/> class from being created.
		/// </summary>
		private PancakeException() : base("Pancake Exception was thrown with no message")
		{
		}

		/// <summary>
		/// Gets the additional data.  Can be used to add any relevant information that should be included.
		/// </summary>
		public object AdditionalData { get; private set; }
	}
}