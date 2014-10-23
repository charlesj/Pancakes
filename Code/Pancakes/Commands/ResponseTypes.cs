namespace Pancakes.Commands
{
	/// <summary>
	/// The types of responses that are possible.
	/// </summary>
	public enum ResponseTypes
	{
		/// <summary>
		/// Success is everything executed correctly.
		/// </summary>
		Success,

		/// <summary>
		/// There was an error executing the response.
		/// </summary>
		Error,

		/// <summary>
		/// The request could not be validated.
		/// </summary>
		InvalidRequest,

		/// <summary>
		/// The request could not be authorized.
		/// </summary>
		Unauthorized,
	}
}