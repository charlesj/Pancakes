namespace Pancakes.Commands
{
    using System;

    using Pancakes.Exceptions;

    /// <summary>
    /// The command not found exception.
    /// </summary>
    public class CommandNotFoundException : PancakeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="types">
        /// The types that were requested.
        /// </param>
        public CommandNotFoundException(Type[] types)
            : base("Unable to Locate Command", types)
        {
            
        }
    }
}
