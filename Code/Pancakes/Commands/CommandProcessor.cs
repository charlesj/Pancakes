using System;
using Pancakes.ServiceLocator;
using Pancakes.Utility;

namespace Pancakes.Commands
{
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

                var commandType = this.commandRegistry.GetRegisteredType(commandName);
                var command = (ICommand) this.serviceLocator.GetService(commandType);
                this.commandSerializer.DeserializeInto(serialization, command);
                return this.commandExecutor.ExecuteAsync(command).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                return new CommandResult {ResultType = CommandResultType.Error};
            }
        }

        private void ValidateArgs(string commandName, string serialization)
        {
            Shield.AgainstNullString(commandName, nameof(commandName));
            Shield.AgainstNullString(serialization, nameof(serialization));
        }
    }
}