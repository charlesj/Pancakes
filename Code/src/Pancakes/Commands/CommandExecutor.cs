using System.Threading.Tasks;

namespace Pancakes.Commands
{
    public class CommandExecutor  : ICommandExecutor
    {
        public async Task<CommandResult> ExecuteAsync(ICommand command)
        {
            if (!command.Validate())
                return new CommandResult {ResultType = CommandResultType.Invalid};
            if (!command.Authorize())
                return new CommandResult() {ResultType = CommandResultType.Unauthorized};

            command.Execute();

            return new CommandResult() {ResultType = CommandResultType.Success};
        }
    }
}