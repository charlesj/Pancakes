namespace Pancakes.Commands
{
	using System;
	using System.Threading.Tasks;

	using Pancakes.Validation;

	/// <summary>
	/// The base data controller.
	/// </summary>
	/// <typeparam name="TRequest">
	/// The Request Object
	/// </typeparam>
	/// <typeparam name="TResult">
	/// The type of the result.
	/// </typeparam>
	public abstract class BaseCommand<TRequest, TResult> where TRequest : Request
	{
		/// <summary>
		/// The valdiator.
		/// </summary>
		private readonly IValidateThings valdiator;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseCommand{TRequest,TResult}"/> class.
		/// </summary>
		/// <param name="valdiator">
		/// The valdiator.
		/// </param>
		protected BaseCommand(IValidateThings valdiator)
		{
			this.valdiator = valdiator;
			this.SuccessMessage = "Wahoo!";
			this.ErrorMessage = "Unspecific Error Message";
			this.ValidationMessage = "The request is invalid";
			this.UnauthorizedMessage = "You are not authorized to make this request.";
		}

		/// <summary>
		/// Gets the request.
		/// </summary>
		protected TRequest Request { get; private set; }

		/// <summary>
		/// Gets or sets A message suitable to displaying to a user on success.
		/// </summary>
		public string SuccessMessage { get; protected set; }

		/// <summary>
		/// Gets or sets A message suitable to displaying to a user on success.
		/// </summary>
		public string ErrorMessage { get; protected set; }

		/// <summary>
		/// Gets or sets the unauthorized message.
		/// </summary>
		public string UnauthorizedMessage { get; protected set; }

		/// <summary>
		/// Gets or sets the validation message.
		/// </summary>
		public string ValidationMessage { get; protected set; }

		/// <summary>
		/// Executes the command
		/// </summary>
		/// <param name="request">
		/// The request.
		/// </param>
		/// <returns>
		/// The <see cref="TResponse"/>.
		/// </returns>
		public async Task<Response<TResult>> Execute(TRequest request)
		{
			this.Request = request;
			var response = new Response<TResult>();
			
			response.Start();
			try
			{
				if (this.Authorize())
				{
					var validationResult = this.valdiator.CheckValidation(request);
					if (validationResult.IsValid)
					{
						response.SetResult(await this.Work());
						response.ResultType = ResponseTypes.Success;
						response.Message = this.SuccessMessage;
					}
					else
					{
						response.ResultType = ResponseTypes.InvalidRequest;
						response.ValidationErrors = validationResult.FailureMessages;
						response.Message = this.ValidationMessage;
					}
				}
				else
				{
					response.ResultType = ResponseTypes.Unauthorized;
					response.Message = this.UnauthorizedMessage;
				}
			}
			catch (Exception exception)
			{
				response.ResultType = ResponseTypes.Error;
				response.Exception = exception;
				response.Message = this.ErrorMessage;
			}

			response.End();
			return response;
		}

		/// <summary>
		/// The do Work.
		/// </summary>
		/// <returns>
		/// The <see cref="TResult"/>.
		/// </returns>
		protected abstract Task<TResult> Work();

		/// <summary>
		/// Runs any authorization check required.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		protected abstract bool Authorize();
	}
}