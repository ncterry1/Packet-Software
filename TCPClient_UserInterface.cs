/*
Let's create a Windows Forms application with a detailed, step-by-step guide, 
focusing on developing a simple user interface on ncterry-server1 that triggers the TcpClientExample functionality.
Step 1: Open Visual Studio

    Launch Visual Studio on ncterry-server1.
    From the Visual Studio start page, select Create a new project.

Step 2: Create a New Windows Forms App Project

    In the Create a new project window, type "Windows Forms" in the search box to filter the project types.
    Select Windows Forms App (.NET Framework) from the list for C#. Click Next.
    Configure your new project:
        Project Name: Enter a name, such as TcpClientUI.
        Location: Choose or confirm the location where your project will be stored.
        Solution Name: It can remain the same as the project name, or you can specify a different name for the solution.
        Click Create.

Step 3: Design the User Interface

    Visual Studio opens your new project, displaying a blank form named Form1.
    Find the Toolbox: If not visible, open it by clicking View > Toolbox.
    Add a Button:
        From the Toolbox, drag a Button control onto the form.
        Position the button where you like on the form.
    Customize the Button:
        With the button selected, find the Properties window. If it's not visible, open it via View > Properties Window.
        Set the Text property to "Send Message".
        Optionally, adjust the Font and ForeColor properties to change the button's appearance.

Step 4: Integrate the TcpClient Code

    Double-click the button on your form. Visual Studio will open the code view and automatically create 
    an event handler for the button's Click event, something like private void button1_Click(object sender, 
    EventArgs e).
    Modify the Event Handler to run the TCP client logic asynchronously. Here's a template, including the 
    TCP client code adjusted for async execution:
END COMMENT*/

// Necessary namespaces for networking and threading
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Define the namespace for the application
namespace TcpClientUI
{
    // Main form class
    public partial class Form1 : Form
    {
        // Constructor for the form
        public Form1()
        {
            InitializeComponent();
        }

        // Event handler for the button click event
        private async void button1_Click(object sender, EventArgs e)
        {
            // Execute the TCP client code on a separate thread to avoid UI freezing
            await Task.Run(() =>
            {
                try
                {
                    // Create a new instance of TcpClient to connect to the server
                    using (TcpClient client = new TcpClient("ncterry-client1", 13000))
                    {
                        // Prepare the message to send
                        string message = "Hello from ncterry-server1";
                        byte[] data = Encoding.ASCII.GetBytes(message);

                        // Get the network stream for writing and reading
                        NetworkStream stream = client.GetStream();

                        // Send the message to the connected TcpServer
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine($"Sent: {message}");

                        // Buffer to store the response bytes
                        data = new byte[256];

                        // String to store the response ASCII representation
                        string responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes
                        int bytes = stream.Read(data, 0, data.Length);
                        responseData = Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine($"Received: {responseData}");

                        // Update the UI with the received response
                        // This must be done on the UI thread
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Received: {responseData}", "Response from Server");
                        }));
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may have occurred and display them on the UI
                    // This also must be done on the UI thread
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error");
                    }));
                }
            });
        }
    }
}
/* START COMMENT
KEY POINTS IN THE CODE
Key Points in the Code:

    >>> Asynchronous Execution: The await Task.Run(() => { ... }) pattern is used to execute the 
    TCP client code asynchronously. This prevents the UI thread from freezing while the application 
    waits for the network operation to complete.
    
    >>> Using TcpClient: An instance of TcpClient is created to manage the connection to the TCP server. 
    The using statement ensures that the TcpClient is properly disposed of once the operation is complete.
    NetworkStream for Communication: The NetworkStream obtained from the TcpClient instance is 
    used for both sending data to the server and receiving data from it.
    
    >>> Invoke for UI Updates: Since the TCP client code runs on a background thread, any updates 
    to the UI (like displaying message boxes) must be done on the UI thread. The Invoke method is 
    used to achieve this, ensuring thread safety.
    
    >>> Error Handling: The try-catch block captures any exceptions that occur during the operation, 
    displaying an error message to the user if something goes wrong.

This detailed walkthrough explains the process of creating a simple TCP client within a 
Windows Forms application, focusing on asynchronous execution and safe UI updates.
END COMMENT*/
/* START COMMENT
Step 5: Build and Run the Application

    Save All: Click File > Save All to ensure your changes are saved.
    Build the Application: Click Build > Build Solution to compile your project.
    Run the Application: Press F5 or click the Start button (green arrow) in Visual Studio to run your application.
    The form you designed will appear with the "Send Message" button. Click the button to execute the TcpClientExample logic, 
    which sends a message to ncterry-client1 and displays the response in a message box.

By following these detailed steps, you've created a Windows Forms application with a simple, professional-looking user 
interface that interacts with a TCP client. This example demonstrates basic asynchronous operation in a UI application 
to maintain responsiveness, a crucial aspect of user interface programming.

END COMMENT */
