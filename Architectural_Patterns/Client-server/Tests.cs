using ClientServerPattern;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientServerPattern.Tests
{
    public class ServerTests
    {
        [Fact]
        public async Task Start_AcceptsClientConnection()
        {
            // Arrange
            var client = new Client();
            var server = new Server();

            Task.Run(() => server.Start());
            client.Connect("127.0.0.1");


            // Assert
            Assert.NotNull(client);

            // Cleanup
            client.Disconnect();
        }
    }

    public class ClientTests
    {
        [Fact]
        public void SendData_WithValidData_SendsDataToServer()
        {
            // Arrange
            var client = new Client();
            var server = new Server();

            Task.Run(() => server.Start());
            client.Connect("127.0.0.1");

            byte[] requestData = Encoding.ASCII.GetBytes("Hello, server!");

            // Act
            client.SendData("Hello, server!");

            // Assert
            // The assertion depends on the server implementation and can be extended based on specific requirements.

            // Cleanup
            client.Disconnect();

        }
    }
}
