using System;
using Autofac;
using Slalom.Stacks.EventHub.Components;
using Slalom.Stacks.EventHub.Settings;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.EventHub.Module
{
    /// <summary>
    /// An Autofac module for the Azure Event Hub Logging block.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class EventHubLoggingModule : Autofac.Module
    {
        private readonly EventHubOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubLoggingModule" /> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public EventHubLoggingModule(EventHubOptions options)
        {
            Argument.NotNull(options, nameof(options));

            _options = options;
        }

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<EventHubPublisher>()
                   .WithParameter(new TypedParameter(typeof(EventHubOptions), _options))
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();
        }
    }
}