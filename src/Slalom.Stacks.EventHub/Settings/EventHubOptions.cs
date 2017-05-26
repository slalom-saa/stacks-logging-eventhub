using System;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.EventHub.Settings
{
    /// <summary>
    /// Options for the Azure Event Hub Logging.
    /// </summary>
    public class EventHubOptions
    {
        /// <summary>
        /// Gets or sets the upper size of the batch to write.  When this number is reached, all items will be written for the given type..
        /// </summary>
        /// <value>The size of the batch used for writing.</value>
        public int BatchSize { get; set; } = 15;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets the events event hub name.
        /// </summary>
        /// <value>The events event hub name.</value>
        public string HubName { get; set; } = "Events";

        /// <summary>
        /// Gets or sets the time between batches.
        /// </summary>
        /// <value>The time between batches.</value>
        public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(5);
    }
}