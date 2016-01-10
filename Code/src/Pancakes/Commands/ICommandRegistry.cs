using System;

namespace Pancakes.Commands
{
    public interface ICommandRegistry
    {
        void Register(Type commandType);
        bool IsRegistered(string commandName);
        Type Locate(string commandName);
        string BuildCommandName(Type commandType);
    }
}