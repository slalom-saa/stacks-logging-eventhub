using System;
using System.Collections.Generic;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.EventHub
{
    /// <summary>
    /// Options for the Azure Event Hub Logging block.
    /// </summary>
    public class EventHubLoggingOptions
    {
        internal string EventHubName { get; set; }

        internal string ConnectionString { get; set; }

        /// <summary>
        /// Sets the connection string to use.
        /// </summary>
        /// <param name="connectionString">The connection string to use.</param>
        /// <returns>Returns this instance for method chaining.</returns>
        public EventHubLoggingOptions WithConnection(string connectionString)
        {
            Argument.NotNullOrWhiteSpace(() => connectionString);

            this.ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// Sets the event hub name to use.
        /// </summary>
        /// <param name="name">The event hub name to use.</param>
        /// <returns>Returns this instance for method chaining.</returns>
        public EventHubLoggingOptions WithEventHubName(string name)
        {
            Argument.NotNullOrWhiteSpace(() => name);

            this.EventHubName = name;
            return this;
        }

    }
}
