/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.EventHub.Components.Batching;
using Slalom.Stacks.EventHub.Settings;
using Slalom.Stacks.Serialization;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.EventHub.Components
{
    /// <summary>
    /// Publishes events to an Azure Event Hub.
    /// </summary>
    /// <seealso href="https://azure.microsoft.com/en-us/services/event-hubs/"/>
    public class EventHubPublisher : PeriodicBatcher<EventEntry>, IEventPublisher
    {
        private readonly Application _application;
        private readonly EventHubClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubPublisher"/> class.
        /// </summary>
        /// <param name="application">The application to use to add information.</param>
        /// <param name="options">The event hub options.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="options"/> argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="application"/> argument is null.</exception>
        public EventHubPublisher(Application application, EventHubOptions options)
            : base(options.BatchSize, options.Period)
        {
            Argument.NotNull(options, nameof(options));
            Argument.NotNull(application, nameof(application));

            _application = application;

            _client = EventHubClient.CreateFromConnectionString(options.ConnectionString + ";EntityPath=" + options.HubName);
        }

        /// <inheritdoc />
        public Task Publish(params EventMessage[] events)
        {
            foreach (var instance in events)
            {
                this.Emit(new EventEntry(instance, _application));
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Emits the batch asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <returns>
        /// Returns a task foir asynchronous programming.
        /// </returns>
        protected override Task EmitBatchAsync(IEnumerable<EventEntry> events)
        {
            var data = events.Select(this.GetEventData);

            return _client.SendAsync(data);
        }

        EventData GetEventData(EventEntry audit)
        {
            var content = JsonConvert.SerializeObject(audit, DefaultSerializationSettings.Instance);
            return new EventData(Encoding.UTF8.GetBytes(content));
        }
    }
}