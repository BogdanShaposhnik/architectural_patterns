using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PeerToPeerPattern.Tests
{
    public class PeerTests
    {
        [Fact]
        public async Task SendMessageAsync_SendsMessageToPeers()
        {
            // Arrange
            var peer1 = new Peer("Peer 1");
            var peer2 = new Peer("Peer 2");
            var peer3 = new Peer("Peer 3");

            var peers = new List<Peer> { peer1, peer2, peer3 };

            // Act
            await peer1.SendMessageAsync("Test Message", peers);

            // Assert
            Assert.Equal(0, peer1.ReceivedMessagesCount);
            Assert.Equal(1, peer2.ReceivedMessagesCount);
            Assert.Equal(1, peer3.ReceivedMessagesCount);
        }

        [Fact]
        public async Task SendMessageAsync_DoesNotSendMessageToSelf()
        {
            // Arrange
            var peer1 = new Peer("Peer 1");
            var peers = new List<Peer> { peer1 };

            // Act
            await peer1.SendMessageAsync("Test Message", peers);

            // Assert
            Assert.Equal(0, peer1.ReceivedMessagesCount);
        }

        [Fact]
        public async Task ReceiveMessageAsync_WaitsForProcessingToFinish()
        {
            // Arrange
            var peer1 = new Peer("Peer 1");
            var peer2 = new Peer("Peer 2");
            var peers = new List<Peer> { peer1, peer2 };

            // Act
            await peer1.SendMessageAsync("Test Message", peers);

            // Assert
            Assert.True(peer2.LastProcessedMessage.EndsWith("Test Message"));
        }

        [Fact]
        public async Task SendMessageAsync_MultipleConcurrentMessages()
        {
            // Arrange
            var peer1 = new Peer("Peer 1");
            var peer2 = new Peer("Peer 2");
            var peer3 = new Peer("Peer 3");
            var peers = new List<Peer> { peer1, peer2, peer3 };

            // Act
            var tasks = new List<Task>
            {
                peer1.SendMessageAsync("Message 1", peers),
                peer2.SendMessageAsync("Message 2", peers),
                peer3.SendMessageAsync("Message 3", peers)
            };

            await Task.WhenAll(tasks);

            // Assert
            Assert.Equal(2, peer1.ReceivedMessagesCount);
            Assert.Equal(2, peer2.ReceivedMessagesCount);
            Assert.Equal(2, peer3.ReceivedMessagesCount);
        }
    }
}
