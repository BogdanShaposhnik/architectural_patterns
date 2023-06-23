using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServerExample
{
    class Server
    {
        public void Start(IPAddress ipAddress, int port)
        {
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            try
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received data from client: {receivedData}");

                string responseData = "Hello from server!";
                byte[] responseBuffer = Encoding.ASCII.GetBytes(responseData);
                stream.Write(responseBuffer, 0, responseBuffer.Length);
                Console.WriteLine("Response sent to client.");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                listener.Stop();
                Console.WriteLine("Server stopped.");
            }
        }
    }

    class Client
    {
        public void Connect(string serverIP, int port)
        {
            try
            {
                TcpClient client = new TcpClient(serverIP, port);
                Console.WriteLine("Connected to server.");

                NetworkStream stream = client.GetStream();

                string data = "Hello from client!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Data sent to server.");

                buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string responseData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received response from server: {responseData}");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            Server server = new Server();
            server.Start(ipAddress, port);
            // Connect as client
            string serverIP = "127.0.0.1";
            Client client = new Client();
            client.Connect(serverIP, port);
        }
    }
}
