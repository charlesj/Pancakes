namespace Pancakes.Commands
{
	public class Headquarters
	{
		private readonly ICommandLocator commandLocator;

		public Headquarters(ICommandLocator commandLocator)
		{
			this.commandLocator = commandLocator;
		}

		public Response<TResult> Execute<TRequest, TResult>(TRequest request) where TRequest : Request
		{
			var command = this.commandLocator.FindCommand<TRequest, TResult>();
			return command.Execute(request);
		}
	}
}