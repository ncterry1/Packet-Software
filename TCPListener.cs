using System; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
namespace TcpListenerExample 

{ 
    class Program 

    { 
        static void Main(string[] args) 

        { 
            int port = 13000; // Port number for the TCP listener 
            IPAddress localAddr = IPAddress.Parse("127.0.0.1"); 

            // TcpListener to accept incoming TCP connection requests. 
            TcpListener server = new TcpListener(localAddr, port); 

            // Start listening for client requests. 
            server.Start(); 

            Console.WriteLine("Waiting for a connection... "); 

            // Perform a blocking call to accept requests. 
            // You could also use server.AcceptTcpClientAsync() for an asynchronous version. 
            TcpClient client = server.AcceptTcpClient(); 
            Console.WriteLine("Connected!"); 

            // Get a stream object for reading and writing 
            NetworkStream stream = client.GetStream(); 

            // Buffer for reading data 
            Byte[] bytes = new Byte[256]; 
            String data = null; 
  
            // Loop to receive all the data sent by the client. 
            int i; 
            while((i = stream.Read(bytes, 0, bytes.Length))!=0) 

            {    
                // Translate data bytes to a ASCII string. 
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i); 
                Console.WriteLine("Received: {0}", data); 

                 
                // Process the data sent by the client. 
                data = data.ToUpper(); 

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data); 

                // Send back a response. 
                stream.Write(msg, 0, msg.Length); 
                Console.WriteLine("Sent: {0}", data);             

            } 
  
            // Shutdown and end connection 
            client.Close(); 
            server.Stop(); 

        } 
    } 
} 
