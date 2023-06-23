using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace EventBusPattern
{
    // Event arguments for the custom event
    public class CustomEventArgs : EventArgs
    {
        public string Message { get; }

        public CustomEventArgs(string message)
        {
            Message = message;
        }
    }

    // Event bus class that manages event subscriptions and event publishing
    public class EventBus
    {
        private Dictionary<Type, List<Delegate>> eventHandlers;

        public EventBus()
        {
            eventHandlers = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<T>(EventHandler<T> handler) where T : EventArgs
        {
            Type eventType = typeof(T);
            if (!eventHandlers.ContainsKey(eventType))
                eventHandlers[eventType] = new List<Delegate>();

            eventHandlers[eventType].Add(handler);
        }

        public void Unsubscribe<T>(EventHandler<T> handler) where T : EventArgs
        {
            Type eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
                eventHandlers[eventType].Remove(handler);
        }

        public void Publish<T>(object sender, T args) where T : EventArgs
        {
            Type eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
            {
                foreach (Delegate handler in eventHandlers[eventType])
                {
                    handler.DynamicInvoke(sender, args);
                }
            }
        }
    }

    // Publisher class that raises events
    public class Publisher
    {
        private EventBus eventBus;

        public Publisher(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        // Method to raise the event
        public void RaiseEvent(string message)
        {
            eventBus.Publish(this, new CustomEventArgs(message));
        }
    }

    // Subscriber class that handles events
    public class Subscriber
    {
        public string Name { get; }
        public int EventCounter { get; set; }

        public Subscriber(string name, EventBus eventBus)
        {
            EventCounter = 0;
            Name = name;
            eventBus.Subscribe<CustomEventArgs>(HandleEvent);
        }

        // Event handler method
        public void HandleEvent(object sender, CustomEventArgs e)
        {
            EventCounter++;
            Console.WriteLine($"Subscriber '{Name}' received message: {e.Message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Event Bus Pattern Example");

            // Create event bus, publisher, and subscribers
            var eventBus = new EventBus();
            var publisher = new Publisher(eventBus);
            var subscriber1 = new Subscriber("Subscriber 1", eventBus);
            var subscriber2 = new Subscriber("Subscriber 2", eventBus);

            // Raise the event
            publisher.RaiseEvent("Hello, subscribers!");

            // Unsubscribe a handler
            eventBus.Unsubscribe<CustomEventArgs>(subscriber2.HandleEvent);

            // Raise the event again
            publisher.RaiseEvent("Event without one subscriber");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
