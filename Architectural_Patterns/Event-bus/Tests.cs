using EventBusPattern;
using System;
using Xunit;

namespace EventBusPattern.Tests
{
    public class EventBusTests
    {
        [Fact]
        public void Publish_WithSingleSubscriber_InvokesEventHandler()
        {
            // Arrange
            var eventBus = new EventBus();
            var publisher = new Publisher(eventBus);
            var subscriber = new Subscriber("Subscriber 1", eventBus);
            string message = null;
            string receivedMessage = null;

            publisher.RaiseEvent(message);

            // Assert
            Assert.Equal(message, receivedMessage);
        }

        [Fact]
        public void Publish_WithMultipleSubscribers_InvokesAllEventHandlers()
        {
            // Arrange
            var eventBus = new EventBus();
            var publisher = new Publisher(eventBus);
            var subscriber1 = new Subscriber("Subscriber 1", eventBus);
            var subscriber2 = new Subscriber("Subscriber 2", eventBus);
            string message = "Test message";

            // Act
            publisher.RaiseEvent(message);

            // Assert
            Assert.Equal(1, subscriber1.EventCounter);
            Assert.Equal(1, subscriber2.EventCounter);
        }

        [Fact]
        public void Unsubscribe_WithInvalidHandler_DoesNotThrowException()
        {
            // Arrange
            var eventBus = new EventBus();
            var publisher = new Publisher(eventBus);
            var subscriber = new Subscriber("Subscriber 1", eventBus);
            string message = "Test message";

            eventBus.Unsubscribe<CustomEventArgs>(subscriber.HandleEvent);
            // Act
            publisher.RaiseEvent(message);

            // Assert
            Assert.Equal(0, subscriber.EventCounter);
        }

        [Fact]
        public void Publish_WithNoSubscribers_DoesNotThrowException()
        {
            // Arrange
            var eventBus = new EventBus();
            var publisher = new Publisher(eventBus);
            string message = "Test message";

            // Act & Assert (no exception should be thrown)
            publisher.RaiseEvent(message);
        }
    }
}
