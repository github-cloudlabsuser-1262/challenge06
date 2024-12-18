# Function to get CPU usage
function Get-CPUUsage {
    $cpu = Get-WmiObject win32_processor | Measure-Object -Property LoadPercentage -Average | Select-Object -ExpandProperty Average
    return "$cpu%"
}

# Function to get memory usage
function Get-MemoryUsage {
    $mem = Get-WmiObject win32_operatingsystem
    $total = [math]::round($mem.TotalVisibleMemorySize / 1MB, 2)
    $free = [math]::round($mem.FreePhysicalMemory / 1MB, 2)
    $used = $total - $free
    $percentUsed = [math]::round(($used / $total) * 100, 2)
    return "Memory Usage: $used/$total GB ($percentUsed%)"
}

# Main loop
while ($true) {
    Write-Output "CPU Usage: $(Get-CPUUsage)"
    Write-Output "$(Get-MemoryUsage)"
    Start-Sleep -Seconds 5
}
