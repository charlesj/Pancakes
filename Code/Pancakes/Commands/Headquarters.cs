namespace Pancakes.Commands
{
	using System.Threading.Tasks;

	public class Headquarters
	{
		private readonly ICommandLocator commandLocator;

		public Headquarters(ICommandLocator commandLocator)
		{
			this.commandLocator = commandLocator;
		}

		public async Task<Response<TResult>> Execute<TRequest, TResult>(TRequest request) where TRequest : Request
		{
			var command = this.commandLocator.FindCommand<TRequest, TResult>();
			return await command.Execute(request);
		}
	}
}