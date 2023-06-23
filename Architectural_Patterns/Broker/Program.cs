using System;
using System.Collections.Generic;

namespace BrokerPattern
{
    // Event Args for the message event
    class MessageEventArgs : EventArgs
    {
        public string Message { get; }

        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }

    // Broker
    class Publisher
    {
        public event EventHandler<MessageEventArgs> MessagePublished;

        public void PublishMessage(string message)
        {
            OnMessagePublished(new MessageEventArgs(message));
        }

        protected virtual void OnMessagePublished(MessageEventArgs e)
        {
            MessagePublished?.Invoke(this, e);
        }
    }

    // Subscriber class
    class Subscriber
    {
        public string Name { get; }
        public List<string> ReceivedMessages { get; } = new List<string>();

        public Subscriber(string name)
        {
            Name = name;
        }

        public void Subscribe(Publisher publisher)
        {
            publisher.MessagePublished += Publisher_MessagePublished;
        }

        public void Unsubscribe(Publisher publisher)
        {
            publisher.MessagePublished -= Publisher_MessagePublished;
        }

        private void Publisher_MessagePublished(object sender, MessageEventArgs e)
        {
            ReceivedMessages.Add(e.Message);
            Console.WriteLine($"{Name} received message: {e.Message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker Pattern Example");

            // Create publisher and subscribers
            var publisher = new Publisher();
            var subscriber1 = new Subscriber("Subscriber 1");
            var subscriber2 = new Subscriber("Subscriber 2");
            var subscriber3 = new Subscriber("Subscriber 3");

            // Subscribe subscribers to the publisher
            subscriber1.Subscribe(publisher);
            subscriber2.Subscribe(publisher);
            subscriber3.Subscribe(publisher);

            // Publish messages
            publisher.PublishMessage("Hello, world!");
            publisher.PublishMessage("This is a test message.");

            // Unsubscribe a subscriber
            subscriber2.Unsubscribe(publisher);

            // Publish another message
            publisher.PublishMessage("Goodbye!");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
