using System;
using System.Linq;
using Slalom.Stacks;
using Slalom.Stacks.EventHub;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Logging;
using Slalom.Stacks.Services.Messaging;

namespace ConsoleClient
{
    public class SomeEvent : Event
    {
        public string Text { get; }

        public SomeEvent(string text)
        {
            this.Text = text;
        }
    }

    [EndPoint("go")]
    public class Go : EndPoint
    {
        public override void Receive()
        {
            this.AddRaisedEvent(new SomeEvent("SSSSddSSS"));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var stack = new Stack())
            {
                stack.UseEventHubPublishing();

                stack.Send("go").Wait();
            }
        }
    }
}