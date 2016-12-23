using System;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.EventHub
{
    /// <summary>
    /// Contains extension methods to add Event Hub Logging blocks.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds the Event Hub Logging block to the container.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="configuration">The configuration routine.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static void UseEventHubLogging(this ApplicationContainer instance, Action<EventHubLoggingOptions> configuration = null)
        {
            Argument.NotNull(instance, nameof(instance));

            var options = new EventHubLoggingOptions();

            configuration?.Invoke(options);

            instance.RegisterModule(new EventHubLoggingModule(options));
        }
    }
}