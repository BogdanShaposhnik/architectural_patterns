using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeerToPeerPattern
{
    class Peer
    {
        public string Name { get; }
        public List<string> ReceivedMessages { get; } = new List<string>();
        public int ReceivedMessagesCount { get; set; }
        public string LastProcessedMessage { get; set; }

        public Peer(string name)
        {
            Name = name;
        }

        public async Task SendMessageAsync(string message, List<Peer> peers)
        {
            Console.WriteLine($"[{Name}] Sending message: {message}");
            await BroadcastMessageAsync(message, peers);
        }

        private async Task BroadcastMessageAsync(string message, List<Peer> peers)
        {
            Console.WriteLine($"[{Name}] Broadcasting message: {message}");

            var tasks = new List<Task>();
            foreach (var peer in peers)
            {
                if (peer != this)
                {
                    tasks.Add(peer.ReceiveMessageAsync(message));
                }
            }

            await Task.WhenAll(tasks);
        }

        public async Task ReceiveMessageAsync(string message)
        {
            Console.WriteLine($"[{Name}] Received message: {message}");
            ReceivedMessages.Add(message);
            await ProcessMessageAsync(message);
        }

        private async Task ProcessMessageAsync(string message)
        {
            ReceivedMessagesCount++;
            LastProcessedMessage = message;
            Console.WriteLine($"[{Name}] Finished processing message: {message}");
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Peer-to-Peer Pattern Example");

            var peer1 = new Peer("Peer 1");
            var peer2 = new Peer("Peer 2");
            var peer3 = new Peer("Peer 3");
            var peers = new List<Peer> { peer1, peer2, peer3 };

            await peer1.SendMessageAsync("Hello, peers!", peers);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
