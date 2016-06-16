using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;

namespace Pancakes.Commands
{
    public class CommandRegistry : ICommandRegistry
    {
        private readonly Dictionary<string, Type> internalRegistry;

        public CommandRegistry()
        {
            this.internalRegistry = new Dictionary<string, Type>();
        }

        public void Register(Type commandType)
        {
            if(!typeof(ICommand).IsAssignableFrom(commandType))
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.InvalidCommandRegistration);

            this.internalRegistry.Add(this.BuildCommandName(commandType), commandType);
        }

        public bool IsRegistered(string commandName)
        {
            return this.internalRegistry.ContainsKey(commandName.ToLowerInvariant());
        }

        public Type GetRegisteredType(string commandName)
        {
            if(string.IsNullOrWhiteSpace(commandName))
                throw new ErrorCodeArgumentNullException(CoreErrorCodes.InvalidCommandLocationString, nameof(commandName));
            return this.internalRegistry[commandName.ToLowerInvariant()];
        }

        public string BuildCommandName(Type commandType)
        {
            return commandType.Name.Replace("Command", string.Empty).ToLowerInvariant();
        }

        public IReadOnlyDictionary<string, Type> GetAllRegisteredCommands()
        {
            return new ReadOnlyDictionary<string, Type>(this.internalRegistry);
        }
    }
}