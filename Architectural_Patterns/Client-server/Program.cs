using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerPattern
{
    // Server class
    public class Server
    {
        private TcpListener listener;

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Console.WriteLine("Server started. Waiting for clients...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        public void HandleClient(TcpClient client)
        {
            try
            {
                Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);

                byte[] buffer = new byte[1024];
                int bytesRead;
                NetworkStream stream = client.GetStream();

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received from client: " + data);

                    // Process the received data
                    string response = ProcessData(data);

                    // Send the response back to the client
                    byte[] responseData = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected: " + client.Client.RemoteEndPoint);
            }
        }

        private string ProcessData(string data)
        {
            // Process the received data and return a response
            return "Processed: " + data.ToUpper();
        }
    }

    // Client class
    public class Client
    {
        private TcpClient client;

        public void Connect(string serverIp)
        {
            client = new TcpClient();
            client.Connect(serverIp, 8888);
            Console.WriteLine("Connected to server: " + client.Client.RemoteEndPoint);
        }

        public void SendData(string data)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
            Console.WriteLine("Sent to server: " + data);

            byte[] responseBuffer = new byte[1024];
            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
            Console.WriteLine("Received from server: " + response);
        }

        public void Disconnect()
        {
            client.Close();
            Console.WriteLine("Disconnected from server.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Start the server
            var server = new Server();
            Task.Run(() => server.Start());

            // Create a client and connect to the server
            var client = new Client();
            client.Connect("127.0.0.1");

            // Send data to the server
            client.SendData("Hello, server!");

            // Disconnect from the server
            client.Disconnect();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
