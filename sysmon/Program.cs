using System.Diagnostics;
using System.Management;

ISystemMetrics systemMetrics = new Win32SystemMetrics();

while (true)
{
    Console.WriteLine($"CPU Usage: {systemMetrics.GetCpuUsage()}%");
    Console.WriteLine(systemMetrics.GetMemoryUsage());
    Thread.Sleep(5000);
}

/// <summary>
/// Interface for system metrics.
/// </summary>
public interface ISystemMetrics
{
    /// <summary>
    /// Gets the CPU usage percentage.
    /// </summary>
    /// <returns>A string representing the CPU usage percentage.</returns>
    string GetCpuUsage();
    
    /// <summary>
    /// Gets the memory usage.
    /// </summary>
    /// <returns>A string representing the memory usage.</returns>
    string GetMemoryUsage();
}

// disable CA1416 because we are using PerformanceCounter which is windows specific and we are okay with that
#pragma warning disable CA1416
public class Win32SystemMetrics : ISystemMetrics
{
    public string GetCpuUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuCounter.NextValue();
        Thread.Sleep(1000); // Allow time for the counter to get a valid reading
        return cpuCounter.NextValue().ToString("0.00");
    }

    public string GetMemoryUsage()
    {
        var memCounter = new PerformanceCounter("Memory", "Available MBytes");
        var availableMemory = memCounter.NextValue();
        var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        var totalMemory = (ulong)searcher.Get().Cast<ManagementObject>().First()["TotalPhysicalMemory"] / (1024 * 1024);
        var usedMemory = totalMemory - availableMemory;
        var percentUsed = (usedMemory / totalMemory) * 100;
        return $"Memory Usage: {usedMemory}/{totalMemory} MB ({percentUsed:0.00}%)";
    }
}
#pragma warning restore CA1416

/// <summary>
/// Class for retrieving system metrics on Linux.
/// </summary>
/// <remarks>
/// We use bash commands to get the CPU and memory usage, and so we assume that the following tools are installed:
///  * top
///  * free
///  * sed/awk
/// </remarks>
public class LinuxSystemMetrics: ISystemMetrics
{
    public string GetCpuUsage()
    {
        var output = ExecuteBashCommand("top -bn1 | grep 'Cpu(s)' | sed 's/.*, *\\([0-9.]*\\)%* id.*/\\1/' | awk '{print 100 - $1}'");
        return output.Trim();
    }

    public string GetMemoryUsage()
    {
        var output = ExecuteBashCommand("free -m | awk 'NR==2{printf \"Memory Usage: %s/%s MB (%.2f%%)\", $3,$2,$3*100/$2 }'");
        return output.Trim();
    }
    
    private string ExecuteBashCommand(string command)
    {
        var escapedArgs = command.Replace("\"", "\\\"");
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        var result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return result;
    }
}
