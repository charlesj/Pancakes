using System.Threading.Tasks;

namespace Pancakes.Commands
{
    public class CommandExecutor  : ICommandExecutor
    {
        public async Task<CommandResult> ExecuteAsync(ICommand command)
        {
            return new CommandResult();
        }
    }
}