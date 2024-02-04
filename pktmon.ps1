<#
When using pktmon on ncterry-client1 to track the communication initiated by your TcpClientExample from ncterry-server1, 
you are essentially capturing TCP packets. After converting the pktmon results to a human-readable form, there are several 
key pieces of information you can look for to identify the associated packets coming from this data transfer:
1. Source and Destination IP Addresses:

    Source IP: This should match the IP address of ncterry-server1, where your TCP client application is running.
    Destination IP: This will be the IP address of ncterry-client1, the machine on which you're running pktmon and the TCP listener.

2. Source and Destination Ports:

    Source Port: This will be a dynamically chosen port from the ephemeral port range of ncterry-server1.
    Destination Port: Since your application uses a specific port (e.g., 13000, as in your example), look for this value as 
    the destination port in the captured packets.

3. TCP Flags:

    Look for packets with the SYN, ACK, or PSH (Push) flags, especially at the beginning and end of the conversation. 
    The initial handshake will involve SYN and ACK flags, while PSH is often used when data is being sent.

4. Payload Size and Content (if applicable):

    Although pktmon does not decode payload content into a human-readable format, you can still observe the size of the payload. 
    Packets carrying your "Hello from ncterry-server1" message will have a larger payload than those just carrying TCP protocol overhead.

Identifying the Packets in pktmon Output

After running pktmon and converting the captured data to a readable format, 
you might see entries similar to the following (simplified for clarity):
#>

#Timestamp: 2021-05-27 12:34:56.789
#Source IP: IP_of_ncterry-server1    Destination IP: IP_of_ncterry-client1
#Source Port: 12345                  Destination Port: 13000
#TCP Flags: SYN, ACK, PSH            Payload Size: XX bytes

<#
Steps to Capture and Analyze with pktmon

    Start Capturing on ncterry-client1:
    Before initiating the communication from ncterry-server1, 
    start packet capture on ncterry-client1 with a command like:
#>

pktmon start --etw -m real-time -p 13000 -c

<#
This command filters the capture to port 13000 and clears previous counters.

Run Your Applications:
First, ensure the TCP listener is running on ncterry-client1. Then, use the UI on ncterry-server1 to send the message.

Stop Capturing and Convert the Log:
After the test, stop the capture with pktmon stop and convert the captured data:
#>

pktmon format pktmon.etl -o readable_log.txt

<#
    Analyze the readable_log.txt:
    Open the generated readable_log.txt file and look for the entries that match the criteria mentioned above.

By focusing on these details, you can identify the packets associated with 
the data transfer initiated by your 
TcpClientExample. This approach allows you to verify the communication flow 
and ensure that your applications are interacting as expected.
#>
