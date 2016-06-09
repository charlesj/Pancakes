using System.Threading.Tasks;

namespace Pancakes.Commands
{
    public class CommandExecutor  : ICommandExecutor
    {
        public async Task<CommandResult> ExecuteAsync(ICommand command)
        {
            if (!await command.ValidateAsync())
                return new CommandResult {ResultType = CommandResultType.Invalid};
            if (!await command.AuthorizeAsync())
                return new CommandResult() {ResultType = CommandResultType.Unauthorized};

            await command.ExecuteAsync();

            return new CommandResult() {ResultType = CommandResultType.Success};
        }
    }
}