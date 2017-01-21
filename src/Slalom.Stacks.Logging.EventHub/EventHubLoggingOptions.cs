using System;
using System.Linq;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.EventHub
{
    /// <summary>
    /// Options for the Azure Event Hub Logging block.
    /// </summary>
    public class EventHubLoggingOptions
    {
        internal int BatchSize { get; set; } = 100;

        internal string ConnectionString { get; set; }

        internal string EventsEventHubName { get; set; } = "Events";

        internal TimeSpan Period { get; set; } = TimeSpan.FromSeconds(5);

        internal string RequestsEventHubName { get; set; } = "Requests";

        /// <summary>
        /// Sets the connection string to use.
        /// </summary>
        /// <param name="connectionString">The connection string to use.</param>
        /// <returns>Returns this instance for method chaining.</returns>
        public EventHubLoggingOptions WithConnection(string connectionString)
        {
            Argument.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            this.ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// Sets the event hub name to use.
        /// </summary>
        /// <param name="events">The events event hub name to use.</param>
        /// <param name="requests">The requests event hub name to use.</param>
        /// <returns>Returns this instance for method chaining.</returns>
        public EventHubLoggingOptions WithEventHubNames(string events, string requests)
        {
            Argument.NotNullOrWhiteSpace(events, nameof(events));

            this.EventsEventHubName = events;
            this.RequestsEventHubName = requests;
            return this;
        }
    }
}