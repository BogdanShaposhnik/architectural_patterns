using System.Net;
using System.Net.Sockets;
using System.Text;
using Xunit;

namespace ClientServerExample.Tests
{
    public class ClientServerTests
    {
        [Fact]
        public void TestServer()
        {
            // Arrange
            Server server = new Server();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            string testData = "Test data";

            // Act
            string receivedData = string.Empty;
            string response = string.Empty;

            // Start the server in a separate task
            var serverTask = System.Threading.Tasks.Task.Run(() =>
            {
                server.Start(ipAddress, port);
            });

            // Connect to the server and send data
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAddress, port);
                NetworkStream stream = client.GetStream();

                // Send test data to the server
                byte[] buffer = Encoding.ASCII.GetBytes(testData);
                stream.Write(buffer, 0, buffer.Length);

                // Receive response from the server
                buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                client.Close();
            }

            // Wait for the server task to complete
            serverTask.Wait();

            // Assert
            Assert.Equal(testData, receivedData);
            Assert.Equal("Hello from server!", receivedData);
        }

        [Fact]
        public void TestClient()
        {
            // Arrange
            Client client = new Client();
            string serverIP = "127.0.0.1";
            int port = 8888;
            string testData = "Test data";

            // Act
            string receivedData = string.Empty;

            // Start the server in a separate task
            var serverTask = System.Threading.Tasks.Task.Run(() =>
            {
                var server = new Server();
                server.Start(IPAddress.Parse(serverIP), port);
            });

            // Connect to the server and send data
            client.Connect(serverIP, port);

            // Wait for the server task to complete
            serverTask.Wait();

            // Assert
            Assert.Equal("Hello from server!", receivedData);
        }
    }
}
