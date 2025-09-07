using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace TextHub.Tests;

/// <summary>
/// Manages the TextHub test server lifecycle
/// </summary>
public class TestServerManager : IDisposable
{
    private Process? _serverProcess;
    private readonly string _projectPath;
    private readonly int _port;
    private readonly string _baseUrl;
    private bool _disposed = false;

    public TestServerManager(string projectPath, int port = 5001)
    {
        _projectPath = projectPath;
        _port = port;
        _baseUrl = $"https://localhost:{_port}";
    }

    public string BaseUrl => _baseUrl;

    /// <summary>
    /// Starts the TextHub application server
    /// </summary>
    public async Task StartAsync()
    {
        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            return; // Already running
        }

        Console.WriteLine($"Starting TextHub server on port {_port}...");

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run --urls https://localhost:5001",
            WorkingDirectory = _projectPath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = false // Keep window visible for debugging
        };

        _serverProcess = Process.Start(startInfo);
        
        if (_serverProcess == null)
        {
            throw new InvalidOperationException("Failed to start the TextHub server process");
        }

        // Wait for the server to be ready
        await WaitForServerReady();
        
        Console.WriteLine($"TextHub server started successfully at {_baseUrl}");
    }

    /// <summary>
    /// Stops the TextHub application server
    /// </summary>
    public void Stop()
    {
        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            Console.WriteLine("Stopping TextHub server...");
            
            try
            {
                _serverProcess.Kill();
                _serverProcess.WaitForExit(5000); // Wait up to 5 seconds
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Error stopping server: {ex.Message}");
            }
            
            _serverProcess.Dispose();
            _serverProcess = null;
        }
    }

    /// <summary>
    /// Waits for the server to be ready by checking if it responds to HTTP requests
    /// </summary>
    private async Task WaitForServerReady()
    {
        var maxAttempts = 30; // 30 seconds timeout
        var attempt = 0;

        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(2);

        while (attempt < maxAttempts)
        {
            try
            {
                var response = await httpClient.GetAsync(_baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    return; // Server is ready
                }
            }
            catch
            {
                // Server not ready yet, continue waiting
            }

            await Task.Delay(1000); // Wait 1 second before next attempt
            attempt++;
        }

        throw new TimeoutException($"TextHub server did not start within {maxAttempts} seconds");
    }

    /// <summary>
    /// Checks if the server is currently running
    /// </summary>
    public bool IsRunning()
    {
        return _serverProcess != null && !_serverProcess.HasExited;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Stop();
            _disposed = true;
        }
    }
}
