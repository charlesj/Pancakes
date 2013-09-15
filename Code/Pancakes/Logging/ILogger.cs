// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The Logging Interface.  It defines 7 different levels of log messages.  It takes two parameters.  The first is
//   message, which should be a short description that is constant for every call to the log.  Any information
//   that changes per call should be passed in via the object metadata.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Logging
{
    /// <summary>
    /// The Logging Interface.  It defines 7 different levels of log messages.  It takes two parameters.  The first is 
    /// message, which should be a short description that is constant for every call to the log.  Any information 
    /// that changes per call should be passed in via the object metadata.
    /// </summary>
    /// <remarks>
    /// The object metadata can be any object - including an anonymous object.  I've found it helpful to create an anonymous
    /// type inline for each call that includes all the useful information.
    /// </remarks>
    /// <example>
    /// ILogger log;
    ///  ...
    /// var newAccountInformation = this.accountActions.CreateAccount(...);
    /// log.Info("AccountCreated", newAccountInformation);
    /// </example>
    public interface ILogger
    {
        /// <summary>
        /// Tracing messages.  These can be used for very low level debugging.  Should be used very rarely.
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        void Trace(string message, object metadata);

        /// <summary>
        /// Debugging messages
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        void Debug(string message, object metadata);

        /// <summary>
        /// Informational messages.  This can be used for auditing purposes so that there is a record
        /// of something happening.
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        void Info(string message, object metadata);

        /// <summary>
        /// Warn is for things that shouldn't happen.
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <remarks>
        /// It's a bit of a judgement call when to use this. One place this seems approrpiate is 
        /// when someone logs in with an incorrect username or password.
        /// </remarks>
        void Warn(string message, object metadata);

        /// <summary>
        /// Error indicates something went wrong, but is recoverable.
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        void Error(string message, object metadata);

        /// <summary>
        /// Fatal indicates an irrecoverable error.
        /// </summary>
        /// <param name="message">
        /// The message.  SHOULD NOT BE DYNAMIC. The message should be a static string that doesn't change at this location.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        void Fatal(string message, object metadata);
    }
}