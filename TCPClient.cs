using System; 
using System.Net.Sockets; 
using System.Text; 
namespace TcpClientExample 

{ 
    class Program 

    { 
        static void Main(string[] args) 

        { 
            try 

            { 
                // Establish the remote endpoint for the socket. 
                // This example uses port 13000 on the local computer. 
                int port = 13000; 
                TcpClient client = new TcpClient("ncterry-client1", port); 

                // Translate the passed message into ASCII and store it as a byte array. 
                string message = "Hello from ncterry-server1"; 
                byte[] data = Encoding.ASCII.GetBytes(message); 

                // Get the stream for writing to the connection 
                NetworkStream stream = client.GetStream(); 

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length); 
                Console.WriteLine("Sent: {0}", message); 

                // Receive the TcpServer.response. 
                // Buffer to store the response bytes. 
                data = new byte[256]; 

                // String to store the response ASCII representation. 
                string responseData = string.Empty; 

                // Read the first batch of the TcpServer response bytes. 
                int bytes = stream.Read(data, 0, data.Length); 
                responseData = Encoding.ASCII.GetString(data, 0, bytes); 
                Console.WriteLine("Received: {0}", responseData); 

                // Close everything. 
                stream.Close(); 
                client.Close(); 

            } 

            catch (ArgumentNullException e) 

            { 
                Console.WriteLine("ArgumentNullException: {0}", e); 

            } 
            catch (SocketException e) 

            { 
                Console.WriteLine("SocketException: {0}", e); 

            } 

            Console.WriteLine("\n Press Enter to continue..."); 
            Console.Read(); 

        } 
    } 
} 
