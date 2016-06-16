using System;
using Pancakes.ServiceLocator;
using Pancakes.Utility;

namespace Pancakes.Commands
{
    public interface ICommandProcessor
    {
        CommandResult Process(string commandName, string serialization);
    }
}