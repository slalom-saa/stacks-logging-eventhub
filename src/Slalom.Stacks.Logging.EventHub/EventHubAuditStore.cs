using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Slalom.Stacks.Messaging.Logging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Logging.EventHub
{
    /// <summary>
    /// An Azure Event Hub <see cref="IAuditStore"/> implementation.
    /// </summary>
    /// <seealso cref="Slalom.Stacks.Messaging.Logging.IAuditStore" />
    /// <seealso cref="System.IDisposable" />
    public class EventHubAuditStore : PeriodicBatcher<AuditEntry>, IAuditStore
    {
        private readonly EventHubClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubAuditStore"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public EventHubAuditStore(EventHubLoggingOptions options)
            : base(options.BatchSize, options.Period)
        {
            Argument.NotNull(options, nameof(options));

            _client = EventHubClient.CreateFromConnectionString(options.ConnectionString + ";EntityPath=" + options.EventHubName);
        }

        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="audit">The audit entry to append.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public async Task AppendAsync(AuditEntry audit)
        {
            this.Emit(audit);
        }

        protected override async Task EmitBatchAsync(IEnumerable<AuditEntry> events)
        {
            var data = events.Select(e => this.GetEventData(e));

            await _client.SendAsync(data);
        }

        EventData GetEventData(AuditEntry audit)

        {
            var content = JsonConvert.SerializeObject(audit);
            return new EventData(Encoding.UTF8.GetBytes(content));
        }
    }
}