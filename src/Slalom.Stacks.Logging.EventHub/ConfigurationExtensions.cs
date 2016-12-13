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
        /// <returns>Returns the container instance for method chaining.</returns>
        public static void UseEventHubLogging(this ApplicationContainer instance)
        {
            Argument.NotNull(instance, nameof(instance));

            instance.RegisterModule(new EventHubLoggingModule());
        }

        /// <summary>
        /// Adds the Event Hub Logging block to the container.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="options">The options to use.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static void UseEventHubLogging(this ApplicationContainer instance, EventHubLoggingOptions options)
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(options, nameof(options));

            instance.RegisterModule(new EventHubLoggingModule(options));
        }

        /// <summary>
        /// Adds the Event Hub Logging block to the container.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <param name="configuration">The configuration routine.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static void UseEventHubLogging(this ApplicationContainer instance, Action<EventHubLoggingOptions> configuration)
        {
            Argument.NotNull(instance, nameof(instance));
            Argument.NotNull(configuration, nameof(configuration));

            instance.RegisterModule(new EventHubLoggingModule(configuration));
        }
    }
}