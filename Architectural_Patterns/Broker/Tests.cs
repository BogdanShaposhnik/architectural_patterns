using System;
using Xunit;

namespace BrokerPattern.Tests
{
    public class BrokerTests
    {
        [Fact]
        public void TestBrokerPattern()
        {
            // Arrange
            var publisher = new Publisher();
            var subscriber1 = new Subscriber("Subscriber 1");
            var subscriber2 = new Subscriber("Subscriber 2");

            // Act
            subscriber1.Subscribe(publisher);
            subscriber2.Subscribe(publisher);

            publisher.PublishMessage("Test Message 1");

            subscriber1.Unsubscribe(publisher);

            publisher.PublishMessage("Test Message 2");

            // Assert
            Assert.Equal(1, subscriber1.ReceivedMessages.Count);
            Assert.Equal(2, subscriber2.ReceivedMessages.Count);
        }
    }
}
