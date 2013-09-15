namespace Pancakes.Logging
{
    using System;

    /// <summary>
    /// Heartbeat is a signal that applications can use to let us know that they are still running.
    /// </summary>
    public class Heartbeat
    {
        /// <summary>
        /// Gets or sets the id.  Autopopulated by RavenDB
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the application instance.
        /// </summary>
        public string ApplicationInstance { get; set; }

        /// <summary>
        /// Gets or sets the last seen.
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// Gets or sets the machine info.
        /// </summary>
        public MachineInfo MachineInfo { get; set; }
    }
}