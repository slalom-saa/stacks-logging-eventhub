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
    public class EventHubPublisher : PeriodicBatcher<EventEntry>, IEventPublisher
    {
        private readonly Application _application;
        private readonly EventHubClient _client;

        public EventHubPublisher(Application application, EventHubOptions options)
            : base(options.BatchSize, options.Period)
        {
            _application = application;
            Argument.NotNull(options, nameof(options));

            _client = EventHubClient.CreateFromConnectionString(options.ConnectionString + ";EntityPath=" + options.HubName);
        }

        public Task Publish(params EventMessage[] events)
        {
            foreach (var instance in events)
            {
                this.Emit(new EventEntry(instance, _application));
            }
            return Task.FromResult(0);
        }

        protected override async Task EmitBatchAsync(IEnumerable<EventEntry> events)
        {

            var data = events.Select(e => this.GetEventData(e));

            await _client.SendAsync(data);
        }

        EventData GetEventData(EventEntry audit)
        {
            var content = JsonConvert.SerializeObject(audit, DefaultSerializationSettings.Instance);
            return new EventData(Encoding.UTF8.GetBytes(content));
        }
    }
}