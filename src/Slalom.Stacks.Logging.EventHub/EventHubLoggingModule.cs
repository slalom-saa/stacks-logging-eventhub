using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.EventHub
{
    /// <summary>
    /// An Autofac module for the Azure Event Hub Logging block.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class EventHubLoggingModule : Module
    {
        private readonly EventHubLoggingOptions _options = new EventHubLoggingOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubLoggingModule"/> class.
        /// </summary>
        public EventHubLoggingModule()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubLoggingModule" /> class.
        /// </summary>
        /// <param name="configuration">The configuration routine.</param>
        public EventHubLoggingModule(Action<EventHubLoggingOptions> configuration)
        {
            Argument.NotNull(configuration, nameof(configuration));

            configuration(_options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubLoggingModule" /> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public EventHubLoggingModule(EventHubLoggingOptions options)
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

            builder.Register(c => new EventHubAuditStore(_options))
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .SingleInstance()
                    .OnPreparing(e =>
                    {
                        var configuration = e.Context.Resolve<IConfiguration>();
                        _options.ConnectionString = configuration.GetOptionalSetting("Stacks:Logging:EventHub:ConnectionString", _options.ConnectionString);
                        _options.EventHubName = configuration.GetOptionalSetting("Stacks:Logging:EventHub:Name", _options.EventHubName);
                    });
        }
    }
}