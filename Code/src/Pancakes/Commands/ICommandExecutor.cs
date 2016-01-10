using System.Threading.Tasks;

namespace Pancakes.Commands
{
    public interface ICommandExecutor
    {
        Task<CommandResult> ExecuteAsync(ICommand command);
    }
}