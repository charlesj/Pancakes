using System;
using Newtonsoft.Json;
using Pancakes.ServiceLocator;
using Pancakes.Utility;

namespace Pancakes.Commands
{
    public interface ICommandProcessor
    {
        CommandResult Process(string commandName, string serialization);
    }

    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandRegistry commandRegistry;
        private readonly ICommandExecutor commandExecutor;
        private readonly ICommandSerializer commandSerializer;
        private readonly IServiceLocator serviceLocator;

        public CommandProcessor(ICommandRegistry commandRegistry, ICommandExecutor commandExecutor, ICommandSerializer commandSerializer, IServiceLocator serviceLocator)
        {
            this.commandRegistry = commandRegistry;
            this.commandExecutor = commandExecutor;
            this.commandSerializer = commandSerializer;
            this.serviceLocator = serviceLocator;
        }

        public CommandResult Process(string commandName, string serialization)
        {
            try
            {
                this.ValidateArgs(commandName, serialization);
                if (!this.commandRegistry.IsRegistered(commandName))
                    return new CommandResult {ResultType = CommandResultType.Unknown};

                var commandType = this.commandRegistry.Locate(commandName);
                var command = (ICommand) this.serviceLocator.GetService(commandType);

            }
            catch (Exception exception)
            {
                return new CommandResult {ResultType = CommandResultType.Error};
            }

            throw new NotImplementedException();
        }

        private void ValidateArgs(string commandName, string serialization)
        {
            Shield.AgainstNullString(commandName, nameof(commandName));
            Shield.AgainstNullString(serialization, nameof(serialization));
        }
    }
}