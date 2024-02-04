<#START COMMMENT
To adapt the network packet capture and analysis workflow for using PowerShell with pktmon, 
here's how you can systematically approach capturing and comparing network packets to understand 
the effect of a driver on data transmission, all through PowerShell commands:
Step 1: Baseline Packet Capture with PowerShell

    Prepare for Capture: Ensure ncterry-server1 and ncterry-client1 are ready for communication.

    Capture Packets on ncterry-client1 Using PowerShell:
        Launch PowerShell as Administrator.
        Begin packet capture with pktmon, focusing on the specific port used by your application 
        (e.g., 13000) or without port filtering to capture all relevant traffic. Use PowerShell 
        to start and manage pktmon:
END COMMENT#>

#powershell
pktmon start --etw -m real-time -p 13000 -c

<#START COMMMENT
Execute the TcpClientExample to generate network traffic.
After running the test, stop the packet capture:
END COMMENT#>

#powershell
pktmon stop

<#START COMMMENT
Convert the capture file to a human-readable format, designating it as the baseline capture:
END COMMENT#>

#powershell
pktmon format pktmon.etl -o baseline_log.txt

<#START COMMMENT
    Review the Baseline Capture: Inspect baseline_log.txt to note the characteristics of the packets.

Step 2: Activate the Driver

    Load Your Driver on ncterry-server1, ensuring it's ready to modify network traffic as intended.

    Verify Driver Functionality: Confirm that the driver is active and functioning correctly, 
    based on any logging or diagnostic outputs it provides.

Step 3: Capture Packets with the Driver Active

    Repeat Packet Capture on ncterry-client1 with the driver now active on the server side:
        Start another packet capture session using PowerShell:
END COMMENT#>

#powershell
pktmon start --etw -m real-time -p 13000 -c

<#START COMMMENT
Run TcpClientExample again to send traffic through the now-active driver.
Stop the packet capture:
END COMMENT#>

#powershell
pktmon stop

<#START COMMMENT
Convert this capture to a readable format, naming it distinctly for comparison:
END COMMENT#>

#powershell
pktmon format pktmon.etl -o post_driver_log.txt
        
<#START COMMMENT
Step 4: Compare the Captures

    Manually Compare the Logs: Open both baseline_log.txt and post_driver_log.txt with a text 
    editor or a diff tool to identify differences in packet details.

    Use Wireshark for Deeper Analysis (Optional): If necessary, import the .etl files into 
    Wireshark for a more granular analysis. This requires converting pktmon captures into a 
    Wireshark-compatible format or capturing the traffic directly with Wireshark using its own capture capabilities.

    Identify Key Differences: Look for changes in the packets that could be attributed to the driver's activity, 
    such as alterations in payload, packet size, or timing.

Step 5: Document and Analyze Findings

    Record Your Observations: Note any consistent changes between the 
    pre- and post-driver activation captures.

    Analyze the Impact: Evaluate how the driver's behavior aligns with its intended 
    function and the observed network traffic modifications.

Considerations

    Controlled Testing Environment: Conduct tests in a stable environment to minimize external influences on your data.
    Repeatability: Perform multiple captures to ensure consistency in your findings.
    Compliance and Ethics: Ensure all your testing and data capturing adhere to legal and ethical standards, 
    particularly regarding privacy and network security.

This workflow leverages PowerShell to orchestrate pktmon for capturing and analyzing 
network traffic, facilitating a comprehensive approach to evaluating the impact of a kernel-mode driver on network communications.
END COMMENT#>
