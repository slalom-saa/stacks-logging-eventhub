/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using Slalom.Stacks.EventHub.Module;
using Slalom.Stacks.EventHub.Settings;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.EventHub
{
    /// <summary>
    /// Contains extension methods to add Event Hub Logging blocks.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Adds the Event Hub Logging block to the container.
        /// </summary>
        /// <param name="instance">The this instance.</param>
        /// <returns>Returns the container instance for method chaining.</returns>
        public static void UseEventHubPublishing(this Stack instance)
        {
            Argument.NotNull(instance, nameof(instance));

            var options = new EventHubOptions();
            instance.Configuration.GetSection("stacks:logging:eventHub")?.Bind(options);

            instance.Use(e => e.RegisterModule(new EventHubLoggingModule(options)));
        }
    }
}