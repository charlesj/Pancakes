using System;
using System.Collections.Generic;

namespace Pancakes.Commands
{
    public interface ICommandRegistry
    {
        void Register(Type commandType);
        bool IsRegistered(string commandName);
        Type GetRegisteredType(string commandName);
        string BuildCommandName(Type commandType);
        IReadOnlyDictionary<string, Type> GetAllRegisteredCommands();
    }
}