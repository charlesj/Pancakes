// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLogLogger.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The implementation of ILogger using NLog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Logging
{
    using NLog;

    using Pancakes.Settings;

    /// <summary>
    /// The implementation of ILogger using NLog.  
    /// </summary>
    public class NLogLogger : ILogger
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger;

        /// <summary>
        /// The application name.
        /// </summary>
        private readonly string applicationName;

        /// <summary>
        /// The application instance.
        /// </summary>
        private readonly string applicationInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public NLogLogger(ISettings settings)
        {
            this.applicationName = settings.ApplicationName;
            this.applicationInstance = settings.ApplicationInstance;
            this.logger = LogManager.GetLogger(this.applicationName);
        }

        /// <summary>
        /// Tracing messages.  These can be used for very low level debugging.  Should be used very rarely.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void Trace(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Trace, message, metadata);
        }

        /// <summary>
        /// Debugging messages
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void Debug(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Debug, message, metadata);
        }

        /// <summary>
        /// Informational messages.  This can be used for auditing purposes so that there is a record
        /// of something happening.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void Info(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Info, message, metadata);
        }

        /// <summary>
        /// Warn is for things that shouldn't happen.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <remarks>
        /// It's a bit of a judgement call when to use this. One place this seems approrpiate is 
        /// when someone logs in with an incorrect username or password.
        /// </remarks>
        public void Warn(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Warn, message, metadata);
        }

        /// <summary>
        /// Error indicates something went wrong, but is recoverable.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void Error(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Error, message, metadata);
        }

        /// <summary>
        /// Fatal indicates an irrecoverable error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void Fatal(string message, object metadata)
        {
            this.WriteLogEntry(LogLevel.Fatal, message, metadata);
        }

        /// <summary>
        /// This method actually makes the call to nlog.
        /// </summary>
        /// <param name="level">
        /// The level of the log entry.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method sets up the nlog event so that it can be logged correctly, including the 
        /// bubbling up of callsite information.  
        /// </para>
        /// <para>
        /// There is one possible bottleneck with this in that is serializes the metadata object straitaway.
        /// This is necessary in order to log that information, but it might also create a performance bottleneck.  If
        /// logging is in need of a performance upgrade, this is one possible place for making it better.  It
        /// could be as simple as just using a more performant json serializer.
        /// </para>
        /// </remarks>
        private void WriteLogEntry(LogLevel level, string message, object metadata)
        {
            var logEvent = new LogEventInfo(level, this.applicationName, message);
            logEvent.Properties["Metadata"] = metadata;
            logEvent.Properties["EventId"] = message;
            logEvent.Properties["InstanceName"] = this.applicationInstance;
            this.logger.Log(typeof(NLogLogger), logEvent);
        }
    }
}