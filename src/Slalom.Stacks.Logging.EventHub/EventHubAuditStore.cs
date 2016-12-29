using System;
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
    public class EventHubAuditStore : IAuditStore, IDisposable
    {
        private readonly EventHubClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubAuditStore"/> class.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public EventHubAuditStore(EventHubLoggingOptions options)
        {
            Argument.NotNull(options, nameof(options));

            _client = EventHubClient.CreateFromConnectionString(options.ConnectionString + ";EntityPath=" + options.EventHubName);
        }

        /// <summary>
        /// Appends an audit with the specified execution elements.
        /// </summary>
        /// <param name="audit">The audit entry to append.</param>
        /// <returns>A task for asynchronous programming.</returns>
        public Task AppendAsync(AuditEntry audit)
        {
            var content = JsonConvert.SerializeObject(audit);
            var eventData = new EventData(Encoding.UTF8.GetBytes(content));
            return _client.SendAsync(eventData);
        }

        #region IDisposable Implementation

        bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="EventHubAuditStore"/> class.
        /// </summary>
        ~EventHubAuditStore()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free other managed objects that implement IDisposable only
                _client.Close();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion
    }
}