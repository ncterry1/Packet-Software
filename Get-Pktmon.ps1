function Start-PktMonCapture {
    param (
        [string]$SourceIP,
        [string]$DestinationIP,
        [int]$Port,
        [switch]$RealTime,
        [int]$Duration
    )

    # Base pktmon command for starting capture
    $pktmonStartCommand = "pktmon start --etw -m real-time"

    # Add filters if specified
    if ($SourceIP) {
        $pktmonStartCommand += " --src-ip $SourceIP"
    }
    if ($DestinationIP) {
        $pktmonStartCommand += " --dest-ip $DestinationIP"
    }
    if ($Port) {
        $pktmonStartCommand += " --port $Port"
    }

    # Enable real-time monitoring if specified
    if ($RealTime.IsPresent) {
        $pktmonStartCommand += " -l real-time"
    } else {
        # Specify a default log file if not in real-time mode
        $logFile = "pktmon-log.etl"
        $pktmonStartCommand += " -o $logFile"
    }

    # Execute the pktmon start command
    Write-Host "Starting pktmon capture: $pktmonStartCommand"
    Invoke-Expression $pktmonStartCommand

    # If Duration is greater than 0, wait for the specified duration and then stop
    if ($Duration -gt 0) {
        $sleepTime = $Duration * 60 # Convert minutes to seconds
        Write-Host "Capturing for $Duration minute(s)."
        Start-Sleep -Seconds $sleepTime

        # Stop the pktmon capture
        $pktmonStopCommand = "pktmon stop"
        Write-Host "Stopping pktmon capture."
        Invoke-Expression $pktmonStopCommand
    } else {
        Write-Host "Packet capture started. To stop, run 'pktmon stop'."
        if (-not $RealTime.IsPresent) {
            Write-Host "Log will be saved to $logFile."
        }
    }
}

# Example usage:
# Start-PktMonCapture -Duration 0 # Starts capture without automatic stop
# Start-PktMonCapture -SourceIP "192.168.1.1" -DestinationIP "192.168.1.2" -Port 80 -RealTime -Duration 5 # Captures for 5 minutes
