namespace Pancakes.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using Pancakes.Validation;

	/// <summary>
	/// The response.
	/// </summary>
	/// <typeparam name="TResult">
	/// The type of the result
	/// </typeparam>
	public class Response<TResult>
	{
		/// <summary>
		/// The stopwatch.  Used for tracking execution times.
		/// </summary>
		private Stopwatch stopwatch;

		/// <summary>
		/// The result.
		/// </summary>
		private TResult result;

		/// <summary>
		/// Initializes a new instance of the <see cref="Response{TResult}"/> class.
		/// </summary>
		public Response()
		{
			this.stopwatch = new Stopwatch();
		}

		/// <summary>
		/// Gets the execution time.
		/// </summary>
		public long ExecutionTime
		{
			get
			{
				return this.stopwatch.ElapsedMilliseconds;
			}
		}

		/// <summary>
		/// Gets the result.
		/// </summary>
		public TResult Result
		{
			get
			{
				return this.result;
			}
		}

		/// <summary>
		/// Gets or sets the exception.
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets or sets the message that could be displayed to the user if the command is successful.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the result type.
		/// </summary>
		public ResponseTypes ResultType { get; set; }

		/// <summary>
		/// Gets or sets the validation errors.
		/// </summary>
		public List<FailureMessage> ValidationErrors { get; set; }

		/// <summary>
		/// The start.
		/// </summary>
		public void Start()
		{
			this.stopwatch.Start();
		}

		/// <summary>
		/// The end.
		/// </summary>
		public void End()
		{
			this.stopwatch.Stop();
		}

		/// <summary>
		/// The set result.
		/// </summary>
		/// <param name="newResult">
		/// The new Result.
		/// </param>
		public void SetResult(TResult newResult)
		{
			this.result = newResult;
		}
	}
}