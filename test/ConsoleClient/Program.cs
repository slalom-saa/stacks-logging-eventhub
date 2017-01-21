using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Slalom.Stacks;
using Slalom.Stacks.Configuration;
using Slalom.Stacks.Logging.EventHub;
using Slalom.Stacks.Test.Examples;
using Slalom.Stacks.Test.Examples.Actors.Items.Add;

// ReSharper disable AccessToDisposedClosure

#pragma warning disable 1998
#pragma warning disable 4014

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var runner = new ExampleRunner(typeof(EventHubAuditStore));
            runner.With(e => e.UseEventHubLogging());
            runner.Start(2);

            Console.WriteLine("Running application.  Press any key to halt...");
            Console.ReadKey();
        }

        
    }
}